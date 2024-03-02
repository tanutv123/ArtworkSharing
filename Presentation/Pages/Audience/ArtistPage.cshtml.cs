using BusinessObject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace Presentation.Pages.Audience
{
    public class ArtistPageModel : PageModel
    {
        private readonly IArtworkRepository _artworkRepository;
        private readonly IUserRepository _userRepository;

        public ArtistPageModel(IArtworkRepository artworkRepository, IUserRepository userRepository)
        {
            _artworkRepository = artworkRepository;
            _userRepository = userRepository;
        }

        public AppUser appUser { get; set; } = default;
        public IEnumerable<Artwork> artworks { get; set; }
        public async Task OnGetAsync(int userid)
        {
            appUser = await _userRepository.GetUserById(userid);
            artworks = await _artworkRepository.GetArtworksByUserId(userid);
        }
    }
}
