using BusinessObject.Entities;
using DataAccess.Management;

namespace Repository;

public class PurchaseRepository : IPurchaseRepository
{
    private readonly PurchaseManagement _purchaseManagement;

    public PurchaseRepository(PurchaseManagement purchaseManagement)
    {
        _purchaseManagement = purchaseManagement;
    }
    
    public IList<Purchase> GetPurchases()
    {
        return _purchaseManagement.GetPurchases();
    }
}