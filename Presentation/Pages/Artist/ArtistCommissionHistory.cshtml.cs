using BusinessObject.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Extensions;
using Repository;

namespace Presentation.Pages.Artist
{
	[Authorize(Policy = "RequireArtistRole")]
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
		public decimal MinPrice { get; set; }
		[BindProperty]
		public decimal MaxPrice { get; set; }
		[BindProperty]
		public string? NotAcceptedReason { get; set; }
		[BindProperty]
		public int? ActualPrice { get; set; }
		public bool IsAcceptSuccess { get; set; } = false;
		public bool IsNotAcceptSuccess { get; set; } = false;
		public bool IsDoneSuccess { get; set; } = false;
		public bool IsInvalidAccept { get; set; } = false;
		public bool IsInvalidReject { get; set; } = false;
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
			CommissionRequestHistoryDTOs = await _commissionRepository.GetCommissionRequestHistoryForArtist(User.GetUserId(), "");
			if(CommissionId == 0)
			{
				ModelState.AddModelError(string.Empty, "Invalid commission");
				IsInvalidAccept = true;
				return Page();
			}
			if (ActualPrice == null || ActualPrice <=0)
			{
				ModelState.AddModelError(string.Empty, "Invalid actual price");
				IsInvalidAccept = true;
				return Page();
			}
			if (ActualPrice < MinPrice || ActualPrice > MaxPrice)
			{
				ModelState.AddModelError(string.Empty, $"Your Price must be between {MinPrice} and {MaxPrice}");
				IsInvalidAccept = true;
				return Page();
			}


			try
            {
                await _commissionRepository.AcceptCommissionRequest(CommissionId, ActualPrice.Value);
				IsInvalidAccept = false;
			}
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
				IsInvalidAccept = true;
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
			CommissionRequestHistoryDTOs = await _commissionRepository.GetCommissionRequestHistoryForArtist(User.GetUserId(), "");
			if (CommissionId == 0)
			{
				ModelState.AddModelError(string.Empty, "Invalid commission");
				IsInvalidReject = true;
				return Page();
			}
			if (string.IsNullOrEmpty(NotAcceptedReason))
			{
				ModelState.ToList().Clear();
				ModelState.AddModelError(string.Empty, "You must fill out the reject reason");
				IsInvalidReject = true;
				return Page();
			}
			try
			{
				await _commissionRepository.NotAcceptCommissionRequest(CommissionId, NotAcceptedReason);
				IsInvalidReject = false;
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
				IsInvalidReject = true;
				return Page();
			}
			return RedirectToPage("/artist/artistcommissionhistory", new
			{
				message = "Commission Denied",
				isNotAcceptSuccess = true,
				commissionId = CommissionId
			});
		}
	}
}
