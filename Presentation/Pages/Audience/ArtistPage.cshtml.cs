using BusinessObject.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Extensions;
using Presentation.Services;
using Repository;

namespace Presentation.Pages.Audience
{
    [Authorize]
    public class ArtistPageModel : PageModel
    {
        private readonly IArtworkRepository _artworkRepository;
        private readonly IUserRepository _userRepository;
        private readonly IImageService _imageService;

        public ArtistPageModel(IArtworkRepository artworkRepository, IUserRepository userRepository, IImageService imageService)
        {
            _artworkRepository = artworkRepository;
            _userRepository = userRepository;
            _imageService = imageService;
        }

        public AppUser appUser { get; set; } = default;
        public IEnumerable<Artwork> artworks { get; set; }
        public int currentUserId = 0;
        public bool currentUserRole = false;
        public async Task OnGetAsync(int userid)
        {
            appUser = await _userRepository.GetUserById(userid);
            artworks = await _artworkRepository.GetArtworksByUserId(userid);
            currentUserId = User.GetUserId();
            currentUserRole = User.IsInRole("Artist");
			foreach (var Artwork in artworks)
			{
				Artwork.ArtworkImage.Url = _imageService.GetImageUploadUrl2(Artwork.ArtworkImage.PublicId);
			}
		}
    }
}
