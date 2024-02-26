using AutoMapper;
using BusinessObject.DTOs;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Extensions;
using Repository;

namespace Presentation.Pages.Audience
{
    [Authorize]
    public class RequestCommissionModel : PageModel
    {
		private readonly ICommissionRepository _commissionRepository;
		private readonly IUserRepository _userRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

		public RequestCommissionModel(
            ICommissionRepository commissionRepository,
            IUserRepository userRepository,
            IGenreRepository genreRepository,
            IMapper mapper
            )
        {
			_commissionRepository = commissionRepository;
			_userRepository = userRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;
		}
        [BindProperty]
        public CommissionRequestDTO CommissionRequestDTO{ get; set; } = new CommissionRequestDTO();
        [BindProperty]
        public string ArtistName { get; set; }
        public string Message { get; set; }
        public List<Genre> Genres { get; set; }
        public async Task OnGetAsync(int artistId)
        {
            if(artistId != null)
            {
                var artist = await _userRepository.GetUserProfile(artistId);
				ArtistName = artist.Name;
                CommissionRequestDTO.SenderId = User.GetUserId();
                CommissionRequestDTO.ReceiverId = artistId;
                Genres = await _genreRepository.GetAll();
            }
            else
            {
                Message = "Something went wrong";
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Genres = await _genreRepository.GetAll();
            if (!ModelState.IsValid) return Page();
            try
            {
                await _commissionRepository.AddCommissionRequest(_mapper.Map<CommissionRequest>(CommissionRequestDTO));
                Message = "Your request has been sent to the artist. Please wait for the artist to reply";
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return Page();
        }
    }
}
