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
		private static TransactionManagement instance = null;
		private static readonly object instanceLock = new object();

		public TransactionManagement() { }

		public static TransactionManagement Instance
		{
			get
			{
				lock (instanceLock)
				{
					if (instance == null)
					{
						instance = new TransactionManagement();
					}
					return instance;
				}
			}
		}
		public async Task<IList<Transaction>> getAllTransactions()
        {
            IList<Transaction> list = new List<Transaction>();
            try
            {
                var _dataContext = new DataContext();
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