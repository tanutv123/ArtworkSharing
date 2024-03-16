using BusinessObject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Extensions;
using Repository;

namespace Presentation.Pages.User;

public class ArtworkList : PageModel
{
    private readonly IPurchaseRepository _purchaseRepository;

    public ArtworkList(IPurchaseRepository purchaseRepository)
    {
        _purchaseRepository = purchaseRepository;
    }
    
    
    [BindProperty]
    public List<Purchase> Purchases { get; set; }
    
    public async Task OnGet()
    {
        Purchases = await _purchaseRepository.GetBuyListForUsers(User.GetUserId());
    }
}