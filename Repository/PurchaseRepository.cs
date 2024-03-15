using BusinessObject.Entities;
using DataAccess.Management;

namespace Repository;

public class PurchaseRepository : IPurchaseRepository
{
    
    public IList<Purchase> GetPurchases()
    {
        return PurchaseManagement.Instance.GetPurchases();
    }
}