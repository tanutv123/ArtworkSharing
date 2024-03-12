using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace Presentation.Pages.Admin.Transaction;

public class TransactionList : PageModel
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionList(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }
    
    [BindProperty]
    public IList<BusinessObject.Entities.Transaction> Transactions { get; set; }

    public async Task OnGetAsync()
    {
        var transactions =  _transactionRepository.GetTransactions();
        if (transactions != null)
        {
            Transactions = await transactions;
        }
    }
}