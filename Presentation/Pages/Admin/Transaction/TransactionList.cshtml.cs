using BusinessObject.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace Presentation.Pages.Admin.Transaction;

public class TransactionList : PageModel
{
    [Authorize(Policy = "RequireAdminRole")]
    public class TransactionListModel : PageModel
    {
        private readonly ICommissionRepository _commissionRepository;

        public TransactionListModel(ICommissionRepository commissionRepository)
        {
            _commissionRepository = commissionRepository;
        }

        [BindProperty] public List<CommissionRequestHistoryDTO> CommissionRequestHistoryDTOs { get; set; }

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