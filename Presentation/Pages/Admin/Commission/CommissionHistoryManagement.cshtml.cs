using BusinessObject.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace Presentation.Pages.Admin.Commission
{
    [Authorize(Policy = "RequireAdminRole")]
    public class CommissionHistoryManagementModel : PageModel
    {
        private readonly ICommissionRepository _commissionRepository;

        public CommissionHistoryManagementModel(ICommissionRepository commissionRepository)
        {
            _commissionRepository = commissionRepository;
        }
        [BindProperty]
        public List<CommissionRequestHistoryAdminDTO> CommissionRequestHistoryDTOs { get; set; }
        public async Task OnGetAsync()
        {
            var history = await _commissionRepository.GetAllCommissionRequestHistory();
            if (history != null)
            {
                CommissionRequestHistoryDTOs = history.ToList();
            }
        }
    }
}
