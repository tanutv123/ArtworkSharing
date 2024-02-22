using BusinessObject.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Extensions;
using Repository;

namespace Presentation.Pages.Artist
{
    public class ArtistCommissionHistoryModel : PageModel
    {
		private readonly ICommissionRepository _commissionRepository;

		public ArtistCommissionHistoryModel(ICommissionRepository commissionRepository)
        {
			_commissionRepository = commissionRepository;
		}
        [BindProperty]
        public List<CommissionRequestHistoryDTO> CommissionRequestHistoryDTOs{ get; set; }
        public async Task OnGet(string statusFilter = "")
        {
            CommissionRequestHistoryDTOs = await _commissionRepository.GetCommissionRequestHistoryForArtist(User.GetUserId(), statusFilter);
        }
    }
}
