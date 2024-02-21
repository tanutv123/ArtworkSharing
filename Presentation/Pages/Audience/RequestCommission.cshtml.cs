using BusinessObject.DTOs;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace Presentation.Pages.Audience
{
    public class RequestCommissionModel : PageModel
    {
		private readonly ICommissionRepository _commissionRepository;

		public RequestCommissionModel(ICommissionRepository commissionRepository)
        {
			_commissionRepository = commissionRepository;
		}
        [BindProperty]
        public CommissionRequestDTO CommissionRequestDTO{ get; set; } = new CommissionRequestDTO();
        public void OnGet(int artistId)
        {
        }
    }
}
