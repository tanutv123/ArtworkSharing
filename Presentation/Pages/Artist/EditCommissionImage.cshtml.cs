using BusinessObject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Extensions;
using Repository;

namespace Presentation.Pages.Artist
{
    public class EditCommissionImageModel : PageModel
    {
		private readonly ICommissionRepository _commissionRepository;

		public EditCommissionImageModel(ICommissionRepository commissionRepository)
        {
			_commissionRepository = commissionRepository;
		}
        [BindProperty]
        public CommissionImage CommissionImage { get; set; }
        public async Task OnGet(int id)
        {
            CommissionImage = await _commissionRepository.GetCommissionImage(id, User.GetUserId());
        }
        public async Task<IActionResult> OnPostEdit()
        {
            if (!ModelState.IsValid) return Page();
            if(string.IsNullOrEmpty(CommissionImage.Description))
            {
                ModelState.AddModelError(string.Empty, "Please enter description");
            }
            try
            {
                await _commissionRepository.EditCommissionImage(CommissionImage);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
            return RedirectToPage("/artist/artistcommissionrequestdetail", new
            {
                id = CommissionImage.CommissionRequestId
            });
        }
		public async Task<IActionResult> OnPostDelete()
		{
			if (!ModelState.IsValid) return Page();
			try
			{
				await _commissionRepository.Delete(CommissionImage);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
				return Page();
			}
			return RedirectToPage("/artist/artistcommissionrequestdetail", new
			{
				id = CommissionImage.CommissionRequestId
			});
		}
	}
}
