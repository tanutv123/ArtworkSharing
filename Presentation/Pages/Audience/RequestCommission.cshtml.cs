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
        public void OnGet()
        {
        }
    }
}
