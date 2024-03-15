using BusinessObject.Entities;
using DataAccess.Management;

namespace Repository;

public class TransactionRepository : ITransactionRepository
{
    public async Task<IList<Transaction>> GetTransactions() => await TransactionManagement.Instance.getAllTransactions();

}