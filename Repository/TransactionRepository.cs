using BusinessObject.Entities;
using DataAccess.Management;

namespace Repository;

public class TransactionRepository : ITransactionRepository
{
    private readonly TransactionManagement _transactionManagement;

    public TransactionRepository(TransactionManagement transactionManagement)
    {
        _transactionManagement = transactionManagement;
    }

    public async Task<IList<Transaction>> GetTransactions() => await _transactionManagement.getAllTransactions();

}