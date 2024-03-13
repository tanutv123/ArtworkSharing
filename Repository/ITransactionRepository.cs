using BusinessObject.Entities;

namespace Repository;

public interface ITransactionRepository
{
    public Task<IList<Transaction>> GetTransactions();
}