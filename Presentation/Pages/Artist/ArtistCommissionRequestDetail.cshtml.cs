using AutoMapper;
using BusinessObject.DTOs;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace Presentation.Pages.Artist
{
    public class ArtistCommissionRequestDetailModel : PageModel
    {
		private readonly ICommissionRepository _commissionRepository;
		private readonly IMapper _mapper;

		public ArtistCommissionRequestDetailModel(
            ICommissionRepository commissionRepository,
            IMapper mapper
            )
        {
			_commissionRepository = commissionRepository;
			_mapper = mapper;
		}
        public List<CommissionImage> CommissionImages{ get; set; }
        [BindProperty]
        public AddCommissionImageDTO AddCommissionImageDTO{ get; set; } = new AddCommissionImageDTO();
        public DateTime CommissionStartDate { get; set; }
        public DateTime CommissionEndDate { get; set; }
        public bool IsAddImageSuccess { get; set; } = false;
        public async Task OnGet(int id, bool isAddImageSuccess = false)
        {
            var commission = await _commissionRepository.GetSingleCommissionRequestHistory(id);
			CommissionStartDate = commission.RequestDate;
			CommissionEndDate = commission.RequestDate.AddDays(30);
			CommissionImages = commission.CommissionImages.OrderBy(x => x.CreatedDate).ToList();
            AddCommissionImageDTO.CommissionRequestId = id;
			IsAddImageSuccess = isAddImageSuccess;
        }

        public async Task<IActionResult> OnPostAddImage()
        {
            if (!ModelState.IsValid)
            {
				var commission1 = await _commissionRepository.GetSingleCommissionRequestHistory(AddCommissionImageDTO.CommissionRequestId);
                if(commission1 != null)
                {
					CommissionImages = commission1.CommissionImages;
					CommissionStartDate = commission1.RequestDate;
					CommissionEndDate = commission1.RequestDate.AddDays(30);
				}
				return Page();
            }
            try
            {
                var commissionImage = _mapper.Map<CommissionImage>(AddCommissionImageDTO);
                if (commissionImage != null)
                {
                    commissionImage.isMain = true;
                    commissionImage.CreatedDate = DateTime.UtcNow;
                    await _commissionRepository.AddCommissionImage(commissionImage);
                }
            }
            catch (Exception ex)
            {
				var commission = await _commissionRepository.GetSingleCommissionRequestHistory(AddCommissionImageDTO.CommissionRequestId);
				CommissionImages = commission.CommissionImages;
				ModelState.AddModelError(string.Empty, ex.Message);
				return Page();
            }
			
			return RedirectToPage("/artist/artistcommissionrequestdetail", new {
				id = AddCommissionImageDTO.CommissionRequestId,
				isAddImageSuccess = true
			});
        }

		public async Task<IActionResult> OnPostDone()
		{
			if (AddCommissionImageDTO.CommissionRequestId == 0)
			{
				var commission1 = await _commissionRepository.GetSingleCommissionRequestHistory(AddCommissionImageDTO.CommissionRequestId);
				if (commission1 != null)
				{
					CommissionImages = commission1.CommissionImages;
				}
				ModelState.AddModelError(string.Empty, "Invalid commission");
				return Page();
			}
			try
			{
				await _commissionRepository.DoneCommissionRequest(AddCommissionImageDTO.CommissionRequestId);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
				var commission = await _commissionRepository.GetSingleCommissionRequestHistory(AddCommissionImageDTO.CommissionRequestId);
				CommissionImages = commission.CommissionImages;
				return Page();
			}
			return RedirectToPage("/artist/artistcommissionhistory", 
				new 
				{
					message = "You have succesfully confirmed that the commission is done", 
					isDoneSuccess = true,
					commissionId = AddCommissionImageDTO.CommissionRequestId
				}
				);
		}
	}
}
