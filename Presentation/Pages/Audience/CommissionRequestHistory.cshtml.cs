using BusinessObject.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Extensions;
using Repository;

namespace Presentation.Pages.Audience
{
    [Authorize]
    public class CommissionRequestHistoryModel : PageModel
    {
		private readonly ICommissionRepository _commissionRepository;

		public CommissionRequestHistoryModel(ICommissionRepository commissionRepository)
        {
			_commissionRepository = commissionRepository;
		}
        public List<CommissionRequestHistoryDTO> CommissionRequestDTOs { get; set; }
        public async Task OnGet(string message = "")
        {
            TempData["Message"] = message;
            CommissionRequestDTOs = await _commissionRepository.GetCommissionRequestHistory(User.GetUserId());
        }
    }
}
