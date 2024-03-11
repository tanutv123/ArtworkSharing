using BusinessObject.Entities;

namespace Repository;

public interface ITransactionRepository
{
    public IList<Transaction> GetTransactions();
}