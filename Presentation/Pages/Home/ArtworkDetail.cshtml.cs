using BusinessObject.DTOs;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Extensions;
using Repository;

namespace Presentation.Pages.Home
{
    public class ArtworkDetailModel : PageModel
    {
        private readonly IArtworkRepository _artworkRepository;

        public ArtworkDetailModel(IArtworkRepository artworkRepository)
        {
            _artworkRepository = artworkRepository;
        }

        public Artwork Artwork { get; set; } = default;
        [BindProperty]
        public AddCommentDTO AddComment { get; set; }

        public IEnumerable<ArtworkComment> ArtworkComment { get; set; }

        public async Task OnGetAsync(int id)
        {
            Artwork = await _artworkRepository.GetArtworkById(id);
            ArtworkComment = await _artworkRepository.GetArtworkComments(id);
        }

        public async Task<IActionResult> OnPostComment()
        {
            Artwork = await _artworkRepository.GetArtworkById(AddComment.Artworkid);
            ArtworkComment = await _artworkRepository.GetArtworkComments(AddComment.Artworkid);
            AddComment.AppUserId = User.GetUserId();
            AddComment.CreatedDate = DateTime.Now;
            AddComment.Content = Request.Form["comment"];

            if (!ModelState.IsValid) return Page();
            try
            {
                await _artworkRepository.AddArtworkComment(AddComment.Artworkid, AddComment.AppUserId, AddComment.Content, AddComment.CreatedDate);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            Artwork = await _artworkRepository.GetArtworkById(AddComment.Artworkid);
            ArtworkComment = await _artworkRepository.GetArtworkComments(AddComment.Artworkid);
            return Page();
        }
    }
}
