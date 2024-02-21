using AutoMapper;
using BusinessObject.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Extensions;
using Repository;

namespace Presentation.Pages.Artist
{
    public class AddCommissionModel : PageModel
    {
		private readonly ICommissionRepository _commissionRepository;
		private readonly IMapper _mapper;

		public AddCommissionModel(
            ICommissionRepository commissionRepository,
            IMapper mapper
            )
        {
			_commissionRepository = commissionRepository;
			_mapper = mapper;
		}
        [BindProperty] 
        public AddCommisionDTO AddCommisionDTO { get; set; } = new AddCommisionDTO();
        public bool IsRegisteredCommission { get; set; }
        public async void OnGetAsync()
        {
            try
            {
                IsRegisteredCommission = await _commissionRepository.CheckArtistRegisterCommission(User.GetUserId());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            AddCommisionDTO.AppUserId = User.GetUserId();
            if (!ModelState.IsValid) return Page();

            try
            {
                var commission = _mapper.Map<BusinessObject.Entities.Commission>(AddCommisionDTO);
                await _commissionRepository.AddCommission(commission);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
            return RedirectToPage("/artist/artistcommission");
        }
    }
}
