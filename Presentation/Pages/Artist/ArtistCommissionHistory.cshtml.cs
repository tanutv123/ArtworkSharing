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
        public List<CommissionRequestHistoryDTO> CommissionRequestHistoryDTOs{ get; set; }
        [BindProperty]
        public int CommissionId { get; set; }
        public async Task OnGet(string statusFilter = "", string message = "")
        {
            CommissionRequestHistoryDTOs = await _commissionRepository.GetCommissionRequestHistoryForArtist(User.GetUserId(), statusFilter);
			TempData["Message"] = message;
		}

        public async Task<IActionResult> OnPostAccept()
        {

			if (!ModelState.IsValid)
			{
				CommissionRequestHistoryDTOs = await _commissionRepository.GetCommissionRequestHistoryForArtist(User.GetUserId(), "");
				return Page();
			}
            try
            {
                await _commissionRepository.AcceptCommissionRequest(CommissionId);
                TempData["Message"] = "Commission Accepted";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
			CommissionRequestHistoryDTOs = await _commissionRepository.GetCommissionRequestHistoryForArtist(User.GetUserId(), "");
			return Page();
        }

		public async Task<IActionResult> OnPostNotAccept()
		{

			if (!ModelState.IsValid)
			{
				CommissionRequestHistoryDTOs = await _commissionRepository.GetCommissionRequestHistoryForArtist(User.GetUserId(), "");
				return Page();
			}
			try
			{
				await _commissionRepository.NotAcceptCommissionRequest(CommissionId);
				TempData["Message"] = "Commission Denied";
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
			}
			CommissionRequestHistoryDTOs = await _commissionRepository.GetCommissionRequestHistoryForArtist(User.GetUserId(), "");
			return Page();
		}
	}
}
