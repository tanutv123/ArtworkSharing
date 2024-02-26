using BusinessObject.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace Presentation.Pages.Audience
{
    public class CommissionRequestHistoryDetailModel : PageModel
    {
        private readonly ICommissionRepository _commissionRepository;

        public CommissionRequestHistoryDetailModel(ICommissionRepository commissionRepository)
        {
            _commissionRepository = commissionRepository;
        }
        public CommissionRequestHistoryDTO CommissionRequestHistoryDTO{ get; set; }
        [BindProperty]
        public int CommissionId { get; set; }
        public async Task OnGet(int id)
        {
            CommissionId = id;
            CommissionRequestHistoryDTO = await _commissionRepository.GetSingleCommissionRequestHistory(id);
        }

        public async Task<IActionResult> OnPost()
        {
			CommissionRequestHistoryDTO = await _commissionRepository.GetSingleCommissionRequestHistory(CommissionId);
			if (!ModelState.IsValid) return Page();

            try
            {
                await _commissionRepository.RequestProgressImage(CommissionId);
                TempData["Message"] = "Request successfully!";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Page();
		}
	}
}
