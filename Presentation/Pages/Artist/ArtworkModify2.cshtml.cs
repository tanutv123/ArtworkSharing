using AutoMapper;
using BusinessObject.DTOs;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Extensions;
using Repository;

namespace Presentation.Pages.Artist
{
    public class ArtworkModify2Model : PageModel
    {
        private readonly IArtworkRepository _artworkRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ArtworkModify2Model(
            IArtworkRepository artworkRepository,
            IMapper mapper,
            IUserRepository userRepository,
            IGenreRepository genreRepository
            )
        {
            _artworkRepository = artworkRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _genreRepository = genreRepository;
        }

        public IEnumerable<Artwork> Artworks { get; set; } = default;
        public IList<ArtworkImage> artworkImage { get; set; }

        [BindProperty]
        public AddArtworkDTO addArtworkDTO { get; set; } = new AddArtworkDTO();
        [BindProperty]
        public AddArtworkImageDTO addArtworkImageDTO { get; set; } = new AddArtworkImageDTO();

        public IEnumerable<Genre> Genres { get; set; }
        public async Task OnGetAsync(int artistid)
        {
            if (artistid != null)
            {
                if (_userRepository != null)
                {
                    var artist = await _userRepository.GetUserById(artistid);
                }

                Genres = await _genreRepository.GetAll();
            }

        }

        public async Task<IActionResult> OnPostAddImage()
        {
            Genres = await _genreRepository.GetAll();
            addArtworkDTO.AppUserId = User.GetUserId();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                addArtworkDTO.CreatedDate = DateTime.UtcNow;
                var artwork = _mapper.Map<Artwork>(addArtworkDTO);
                addArtworkImageDTO.isMain = true;
                artwork.ArtworkImage = _mapper.Map<ArtworkImage>(addArtworkImageDTO);
                await _artworkRepository.AddArtwork(artwork);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Page();
        }
    }
}
