using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObject.Entities;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Management
{
    public class TransactionManagement
    {
        private readonly DataContext _dataContext;

        public TransactionManagement(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IList<Transaction>> getAllTransactions()
        {
            IList<Transaction> list = new List<Transaction>();
            try
            {
                list = await _dataContext.Transactions
                    .Include(t => t.Receiver)
                    .Include(t => t.Sender)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return list;
        }
    }
}