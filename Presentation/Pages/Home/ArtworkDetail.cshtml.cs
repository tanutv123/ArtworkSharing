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
        [BindProperty]
        public AddLikeDTO AddLikeDTO { get; set; }
        [BindProperty]
        public AddFollowDTO AddFollowDTO { get; set; }
        public IEnumerable<ArtworkComment> ArtworkComment { get; set; }
        public bool Likes { get; set; }
        public int UserId { get; set; }


        public async Task OnGetAsync(int id)
        {
            Artwork = await _artworkRepository.GetArtworkById(id);
            ArtworkComment = await _artworkRepository.GetArtworkComments(id);
            if (Artwork != null)
            {
                Likes = await _artworkRepository.HasUserLikedArtwork(User.GetUserId(), id);
            }
            UserId = User.GetUserId();
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

        public async Task<IActionResult> OnPostLike()
        {
            Artwork = await _artworkRepository.GetArtworkById(AddLikeDTO.ArtworkId);
            ArtworkComment = await _artworkRepository.GetArtworkComments(AddLikeDTO.ArtworkId);
            if (Artwork != null)
            {
                Likes = await _artworkRepository.HasUserLikedArtwork(User.GetUserId(), AddLikeDTO.ArtworkId);
            }
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
            Artwork = await _artworkRepository.GetArtworkById(Artwork.Id);
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
        //Do it Later
        public async Task<IActionResult> OnPostBuy()
        {
            Artwork = await _artworkRepository.GetArtworkById(Artwork.Id);
            if (AddFollowDTO != null)
            {
                AddFollowDTO.AppUserId = User.GetUserId();
            } 
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
