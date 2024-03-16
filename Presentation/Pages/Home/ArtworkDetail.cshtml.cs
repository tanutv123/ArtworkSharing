using BusinessObject.DTOs;
using BusinessObject.Entities;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Extensions;
using Presentation.Services;
using Repository;

namespace Presentation.Pages.Home
{
    [Authorize]
    public class ArtworkDetailModel : PageModel
    {
        private readonly IArtworkRepository _artworkRepository;
        private readonly IImageService _imageService;

        public ArtworkDetailModel(IArtworkRepository artworkRepository, IImageService imageService)
        {
            _artworkRepository = artworkRepository;
            _imageService = imageService;
        }

        public Artwork Artwork { get; set; } = default;
        [BindProperty]
        public AddCommentDTO AddComment { get; set; }
        [BindProperty]
        public AddLikeDTO AddLikeDTO { get; set; }
        [BindProperty]
        public AddFollowDTO AddFollowDTO { get; set; }
        [BindProperty]
        public AddPurchaseDTO addPurchaseDTO { get; set; }
        [BindProperty]
        public AddTransationDTO addTransationDTO{ get; set; }
        public IEnumerable<ArtworkComment> ArtworkComment { get; set; }
        public bool Likes { get; set; }
        public int UserId { get; set; }
        public bool Follows { get; set; }


        public async Task OnGetAsync(int id)
        {
            Artwork = await _artworkRepository.GetArtworkById(id);
            Artwork.ArtworkImage.Url = _imageService.GetImageUploadUrl2(Artwork.ArtworkImage.Url);
            ArtworkComment = await _artworkRepository.GetArtworkComments(id);
            if (Artwork != null)
            {
                Likes = await _artworkRepository.HasUserLikedArtwork(User.GetUserId(), id);
                Follows = await _artworkRepository.HasUserFollowed(User.GetUserId(), Artwork.AppUserId);
            }
            UserId = User.GetUserId();
        }

        public async Task<IActionResult> OnPostComment()
        {
            //Keep Page State
            Artwork = await _artworkRepository.GetArtworkById(AddComment.Artworkid);
            ArtworkComment = await _artworkRepository.GetArtworkComments(AddComment.Artworkid);
            if (Artwork != null)
            {
                Likes = await _artworkRepository.HasUserLikedArtwork(User.GetUserId(), AddComment.Artworkid);
                Follows = await _artworkRepository.HasUserFollowed(User.GetUserId(), Artwork.AppUserId);
            }
            UserId = User.GetUserId();
            //
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
            return Page();
        }

        public async Task<IActionResult> OnPostLike()
        {
            //Keep Page State
            Artwork = await _artworkRepository.GetArtworkById(AddLikeDTO.ArtworkId);
            ArtworkComment = await _artworkRepository.GetArtworkComments(AddLikeDTO.ArtworkId);
            
            UserId = User.GetUserId();
            //
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
            if (Artwork != null)
            {
                Likes = await _artworkRepository.HasUserLikedArtwork(User.GetUserId(), AddLikeDTO.ArtworkId);
                Follows = await _artworkRepository.HasUserFollowed(User.GetUserId(), Artwork.AppUserId);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostFollow()
        {
            //Keep Page State
            Artwork = await _artworkRepository.GetArtworkById(AddLikeDTO.ArtworkId);
            ArtworkComment = await _artworkRepository.GetArtworkComments(AddLikeDTO.ArtworkId);
            
            UserId = User.GetUserId();
            //
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
            if (Artwork != null)
            {
                Likes = await _artworkRepository.HasUserLikedArtwork(User.GetUserId(), AddLikeDTO.ArtworkId);
                Follows = await _artworkRepository.HasUserFollowed(User.GetUserId(), Artwork.AppUserId);
            }
            return Page();
        }
        
        public async Task<IActionResult> OnPostBuy()
        {
            //Keep Page State
            Artwork = await _artworkRepository.GetArtworkById(AddLikeDTO.ArtworkId);
            ArtworkComment = await _artworkRepository.GetArtworkComments(AddLikeDTO.ArtworkId);
            if (Artwork != null)
            {
                Likes = await _artworkRepository.HasUserLikedArtwork(User.GetUserId(), AddLikeDTO.ArtworkId);
                Follows = await _artworkRepository.HasUserFollowed(User.GetUserId(), Artwork.AppUserId);
            }
            UserId = User.GetUserId();
            //
            addPurchaseDTO.ArtworkId = AddLikeDTO.ArtworkId;
            addTransationDTO.SenderId = User.GetUserId();
            addTransationDTO.ReceiverId = Artwork.AppUserId;
            addTransationDTO.Money = addPurchaseDTO.BuyPrice;
            if (addPurchaseDTO != null)
            {
                addPurchaseDTO.AppUserId = User.GetUserId();
            } 
            if (!ModelState.IsValid) return Page();
            try
            {
                await _artworkRepository.BuyArtwork(addPurchaseDTO,addTransationDTO);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return Page();
        }
    }
}
