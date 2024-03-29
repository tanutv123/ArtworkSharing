using AutoMapper;
using BusinessObject.DTOs;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Extensions;
using Presentation.Services;
using Repository;

namespace Presentation.Pages.Artist
{
    [Authorize(Policy = "RequireArtistRole")]
    public class ArtworkModifyModel : PageModel
    {
		private readonly IArtworkRepository _artworkRepository;
		private readonly IGenreRepository _genreRepository;
		private readonly IUserRepository _userRepository;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;


		public ArtworkModifyModel(
			IArtworkRepository artworkRepository,
			IMapper mapper,
			IUserRepository userRepository,
			IImageService imageService,
			IGenreRepository genreRepository
			)
		{
			_artworkRepository = artworkRepository;
			_mapper = mapper;
			_userRepository = userRepository;
            _imageService = imageService;
            _genreRepository = genreRepository;
		}

		public Artwork Artwork { get; set; } = default;
		public IList<ArtworkImage> artworkImage { get; set; }

		[BindProperty]
		public AddArtworkDTO addArtworkDTO { get; set; } = new AddArtworkDTO();
		[BindProperty]
		public AddArtworkImageDTO addArtworkImageDTO { get; set; } = new AddArtworkImageDTO();

		public IEnumerable<Genre> Genres { get; set; }
		public int currentUserId = 0;
        public async Task OnGetAsync(int artworkid)
		{
			Artwork = await _artworkRepository.GetArtworkById(artworkid);
			currentUserId = User.GetUserId();
            addArtworkDTO.Id = artworkid;
            if (Artwork != null)
			{
                if (_userRepository != null)
				{
					var artist = await _userRepository.GetUserById(User.GetUserId());
				}

				Genres = await _genreRepository.GetAll();
			}

		}

		public async Task<IActionResult> OnPostDeleteArtwork()
		{
			Genres = await _genreRepository.GetAll();
			addArtworkDTO.AppUserId = User.GetUserId();
            int artworkid = int.Parse(Request.Form["artworkId"]);
            string publicId = Request.Form["publicId"];

            try
			{
				var artwork = _mapper.Map<Artwork>(addArtworkDTO);
                await _artworkRepository.DeleteArtwork(artworkid);
                await _imageService.DeletePhotoAsync(publicId);
                TempData["Message"] = "Artwork has been Deleted successfully!";
                return Redirect("/Audience/ArtistPage?userId=" + User.GetUserId());

            }
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
			}
            currentUserId = User.GetUserId();
            return Page();
		}
	}
}

