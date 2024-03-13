using BusinessObject.Entities;

namespace Repository;

public interface IPurchaseRepository
{
    public IList<Purchase> GetPurchases();

}