using BusinessObject.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Extensions;
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
        public bool IsRequestSentSuccess { get; set; } = false;
        public bool IsInvalidAccess { get; set; } = true;
        public async Task OnGet(int id, string message = null, bool isRequestSuccess = false)
        {
            var commission = await _commissionRepository.GetSingleCommissionRequestHistory(id);
            if (commission != null && commission.SenderEmail != User.GetEmailAddress()) 
            {
                IsInvalidAccess = true;
            }
            else
            {
                IsInvalidAccess = false;
				TempData["Message"] = message;
				IsRequestSentSuccess = isRequestSuccess;
				CommissionId = id;
				CommissionRequestHistoryDTO = await _commissionRepository.GetSingleCommissionRequestHistory(id);
			}
        }

        public async Task<IActionResult> OnPost()
        {
            IsRequestSentSuccess = false;
			CommissionRequestHistoryDTO = await _commissionRepository.GetSingleCommissionRequestHistory(CommissionId);
			if (!ModelState.IsValid) return Page();

            try
            {
                await _commissionRepository.RequestProgressImage(CommissionId);
                IsRequestSentSuccess = true;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }

            return RedirectToPage("/audience/commissionrequesthistorydetail", new
            {
                id = CommissionId,
                message = "Request successfully!",
                isRequestSuccess = IsRequestSentSuccess
            });
        }
	}
}
