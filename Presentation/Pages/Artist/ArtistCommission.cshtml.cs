using BusinessObject.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Extensions;
using Repository;

namespace Presentation.Pages.Artist
{
    [Authorize(Policy = "RequireArtistRole")]
    public class ArtistCommissionModel : PageModel
    {
        private readonly ICommissionRepository _commissionRepository;

        public ArtistCommissionModel(ICommissionRepository commissionRepository)
        {
            _commissionRepository = commissionRepository;
        }
        public Commission Commission { get; set; }
        public async Task OnGetAsync()
        {
            Commission = await _commissionRepository.GetArtistCommission(User.GetUserId());
        }
    }
}
