using BusinessObject.DTOs;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Presentation.Extensions;
using Repository;

namespace Presentation.Pages.Home
{


    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IArtworkRepository _artworkRepository;

        public IndexModel(IArtworkRepository artworkRepository)
        {
            _artworkRepository = artworkRepository;
        }

        public IEnumerable<Artwork> Artworks { get; set; } = default;
        [BindProperty]
        public AddLikeDTO AddLikeDTO { get; set; }
        [BindProperty]
        public AddFollowDTO AddFollowDTO { get; set; }
        public async Task OnGetAsync()
        {
            Artworks = await _artworkRepository.GetArtworks();

        }

        public async Task<IActionResult> Index(string title)
        {
            if (String.IsNullOrEmpty(title))
            {
                Artworks = await _artworkRepository.GetArtworks();
            }
            else
            {
                Artworks = await _artworkRepository.GetArtworkByTitle(title);
            }
            return Page();

        }

        public async Task<IActionResult> OnPostLike()
        {
            Artworks = await _artworkRepository.GetArtworks();
            AddLikeDTO.AppUserId = User.GetUserId();
            if (!ModelState.IsValid) return Page();
            try
            {
                await _artworkRepository.LikeArtwork(AddLikeDTO.AppUserId, AddLikeDTO.ArtworkId);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostFollow()
        {
            Artworks = await _artworkRepository.GetArtworks();
            if (AddFollowDTO != null)
            {
                AddFollowDTO.AppUserId = User.GetUserId();
            } //When Debug reach here. TargetUserId was 0 and AppUserId was 13.
              //I was expecting TargerUserId to be 4 but no its 0
            if (!ModelState.IsValid) return Page();
            try
            {
                await _artworkRepository.FollowArtist(AddFollowDTO.TargetUserId, AddFollowDTO.AppUserId);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return Page();
        }
    }
}
