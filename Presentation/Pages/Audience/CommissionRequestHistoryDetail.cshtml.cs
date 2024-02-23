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
        public async Task OnGet(int id)
        {
            CommissionRequestHistoryDTO = await _commissionRepository.GetSingleCommissionRequestHistory(id);
        }
    }
}
