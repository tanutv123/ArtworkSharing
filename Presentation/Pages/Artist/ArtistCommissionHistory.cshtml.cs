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
		[BindProperty]
		public string? NotAcceptedReason { get; set; }
		[BindProperty]
		public int? ActualPrice { get; set; }
		public bool IsAcceptSuccess { get; set; } = false;
		public bool IsNotAcceptSuccess { get; set; } = false;
		public bool IsDoneSuccess { get; set; } = false;
        public async Task OnGet(int commissionId = 0, string statusFilter = "", string message = "", bool isAcceptSuccess = false, bool isNotAcceptSuccess = false, bool isDoneSuccess = false)
        {
            CommissionRequestHistoryDTOs = await _commissionRepository.GetCommissionRequestHistoryForArtist(User.GetUserId(), statusFilter);
			TempData["Message"] = message;
			IsNotAcceptSuccess = isNotAcceptSuccess;
			IsAcceptSuccess = isAcceptSuccess;
			IsDoneSuccess = isDoneSuccess;
			if(commissionId != 0)
			{
				CommissionId = commissionId;
			}
		}

        public async Task<IActionResult> OnPostAccept()
        {
			if(!ModelState.IsValid)
			{
				CommissionRequestHistoryDTOs = await _commissionRepository.GetCommissionRequestHistoryForArtist(User.GetUserId(), "");
				return Page();
			}
			if (ActualPrice <= 0)
			{
				CommissionRequestHistoryDTOs = await _commissionRepository.GetCommissionRequestHistoryForArtist(User.GetUserId(), "");
				ModelState.AddModelError(string.Empty, "Invalid actual price");
				return Page();
			}


            try
            {
                await _commissionRepository.AcceptCommissionRequest(CommissionId, ActualPrice.Value);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
				return Page();
            }
			CommissionRequestHistoryDTOs = await _commissionRepository.GetCommissionRequestHistoryForArtist(User.GetUserId(), "");
			return RedirectToPage("/artist/artistcommissionhistory", new
			{
				message = "Commission Accepted",
				isAcceptSuccess = true,
				commissionId = CommissionId
			});
        }

		public async Task<IActionResult> OnPostNotAccept()
		{

			if (!ModelState.IsValid)
			{
				CommissionRequestHistoryDTOs = await _commissionRepository.GetCommissionRequestHistoryForArtist(User.GetUserId(), "");
				return Page();
			}
			if(string.IsNullOrEmpty(NotAcceptedReason))
			{
				ModelState.AddModelError(string.Empty, "You must fill out the reject reason");
				return Page();
			}
			try
			{
				await _commissionRepository.NotAcceptCommissionRequest(CommissionId, NotAcceptedReason);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
			}
			CommissionRequestHistoryDTOs = await _commissionRepository.GetCommissionRequestHistoryForArtist(User.GetUserId(), "");
			return RedirectToPage("/artist/artistcommissionhistory", new
			{
				message = "Commission Denied",
				isNotAcceptSuccess = true,
				commissionId = CommissionId
			});
		}
	}
}
