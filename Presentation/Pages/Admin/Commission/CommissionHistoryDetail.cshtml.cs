using BusinessObject.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Diagnostics;

namespace Presentation.Pages.Admin.Commission
{
    public class CommissionHistoryDetailModel : PageModel
    {
        private readonly ICommissionRepository _commissionRepository;
        public CommissionHistoryDetailModel(ICommissionRepository commissionRepository)
        {
            _commissionRepository = commissionRepository;
        }
        public CommissionRequestHistoryAdminDTO commissionRequestHistoryDTO { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var detail = await _commissionRepository.GetSingleCommissionRequestHistoryAdmin(id);
            if(detail != null)
            {
                commissionRequestHistoryDTO = detail;
            }
            return Page();
        }
    }
}
