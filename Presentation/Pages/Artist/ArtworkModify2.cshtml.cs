using AutoMapper;
using BusinessObject.DTOs;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Extensions;
using Repository;

namespace Presentation.Pages.Artist
{
    [Authorize(Policy = "RequireArtistRole")]
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

        public Artwork Artwork { get; set; } = default;
        public IList<ArtworkImage> artworkImage { get; set; }

        [BindProperty]
        public AddArtworkDTO addArtworkDTO { get; set; } 
        [BindProperty]
        public UpdateArtworkImageDTO addArtworkImageDTO { get; set; } = new UpdateArtworkImageDTO();

        public IEnumerable<Genre> Genres { get; set; }
        public async Task OnGetAsync(int artworkid)
        {
            if (artworkid != null)
            {
                if (_artworkRepository != null)
                {
                    Artwork = await _artworkRepository.GetArtworkById(artworkid);
                    addArtworkDTO = new AddArtworkDTO();
                    addArtworkDTO.Title = Artwork.Title;
                    addArtworkDTO.Price = Artwork.Price;
                    addArtworkDTO.CreatedDate = Artwork.CreatedDate;
                    addArtworkDTO.Description = Artwork.Description;
                    addArtworkDTO.AppUserId = Artwork.AppUserId;
                    addArtworkDTO.Id = artworkid;
                    addArtworkImageDTO.ArtworkId = artworkid.ToString();
                    addArtworkImageDTO.Url = Artwork.ArtworkImage.Url;
                    addArtworkImageDTO.PublicId = Artwork.ArtworkImage.PublicId;
                    addArtworkImageDTO.Id = Artwork.ArtworkImage.Id;
                    
                }

                Genres = await _genreRepository.GetAll();
            }

        }

        public async Task<IActionResult> OnPostUpdateImage()
        {
            Genres = await _genreRepository.GetAll();
            addArtworkDTO.AppUserId = User.GetUserId();
            addArtworkImageDTO.ArtworkId = addArtworkDTO.Id.ToString();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                addArtworkDTO.CreatedDate = DateTime.UtcNow;
                var artwork = _mapper.Map<Artwork>(addArtworkDTO);
                addArtworkImageDTO.isMain = true;
                var artworkimg = _mapper.Map<ArtworkImage>(addArtworkImageDTO);
                await _artworkRepository.UpdateArtwork(artwork);
                await _artworkRepository.UpdateArtworkImage(artworkimg);
				TempData["Message"] = "Artwork has been Updated successfully!";
				return Redirect("/Audience/ArtistPage?userId=" + User.GetUserId());
			}
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Page();
        }
    }
}
