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
        [BindProperty]
        public string ArtistEmail { get; set; }
        public string Message { get; set; }
        public bool IsRequestSentSuccess { get; set; } = false;
        public List<Genre> Genres { get; set; }
        public async Task OnGetAsync(int artistId, string message = null, bool isRequestSuccess = false)
        {
            IsRequestSentSuccess = isRequestSuccess;
            Message = message;
            Genres = await _genreRepository.GetAll();
            if (artistId != 0)
            {
                var artist = await _userRepository.GetUserProfile(artistId);
				ArtistName = artist.Name;
                ArtistEmail = artist.Email;
                CommissionRequestDTO.SenderId = User.GetUserId();
                CommissionRequestDTO.ReceiverId = artistId;
                
            }
            else
            {
                
                Message = "Something went wrong";
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            IsRequestSentSuccess = false;
            Genres = await _genreRepository.GetAll();
            if (!ModelState.IsValid) return Page();
            
            try
            {
				if (CommissionRequestDTO.MinPrice <= 0 && CommissionRequestDTO.MaxPrice <= 0)
				{
					throw new Exception("Price must be greater than 0");
				}
				else if (CommissionRequestDTO.MaxPrice == CommissionRequestDTO.MinPrice || CommissionRequestDTO.MinPrice > CommissionRequestDTO.MaxPrice)
				{
					throw new Exception("Max Price must be greater than Min price");
				}
				if (CommissionRequestDTO.DueDate.Date == DateTime.UtcNow.Date)
                {
					throw new Exception("Due Date must be ahead from current date");
				}
                await _commissionRepository.AddCommissionRequest(_mapper.Map<CommissionRequest>(CommissionRequestDTO));
                IsRequestSentSuccess = true;
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
            return RedirectToPage("/audience/requestcommission", new {
                artistId = CommissionRequestDTO.ReceiverId, 
                message = "Your request has been sent to the artist. Please wait for the artist to reply",
                isRequestSuccess = IsRequestSentSuccess
            });
        }
    }
}
