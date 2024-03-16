using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace Presentation.Pages.Admin.Purchase;

public class PurchaseList : PageModel
{
    private readonly IPurchaseRepository _purchaseRepository;
    
    public PurchaseList(IPurchaseRepository purchaseRepository)
    {
        _purchaseRepository = purchaseRepository;
    }

    [BindProperty]
    public List<BusinessObject.Entities.Purchase> Purchases { set; get; } 
    
    [BindProperty]
    public decimal totalIncome { get; set; }
    
    public async Task OnGet()
    {
        var list =  _purchaseRepository.GetPurchases().ToList();
        Purchases = list;
        foreach (var purchase in list)
        {
            decimal profit = (purchase.BuyPrice * 10 / 100);
            totalIncome += profit;
        }
    }
    

    
    
    
}