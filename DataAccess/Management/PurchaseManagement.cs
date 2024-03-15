using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessObject.Entities;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Management
{
    public class PurchaseManagement
    {
		private static PurchaseManagement instance = null;
		private static readonly object instanceLock = new object();

		public PurchaseManagement() { }

		public static PurchaseManagement Instance
		{
			get
			{
				lock (instanceLock)
				{
					if (instance == null)
					{
						instance = new PurchaseManagement();
					}
					return instance;
				}
			}
		}

		public IQueryable<Purchase> GetAllAsQueryable() 
        {
			var _dataContext = new DataContext();
			return _dataContext.Purchases.AsQueryable();
        }


        public void AddPurchase(Purchase purchase)
        {
            try
            {
                var _dataContext = new DataContext();
                if (_dataContext != null && _dataContext.Purchases != null)
                {
                    _dataContext.Purchases.Add(purchase);
                    _dataContext.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public List<Purchase> GetPurchases()
        {
            var purchases = new List<Purchase>();
            try
            {
				var _dataContext = new DataContext();
				if (_dataContext != null && _dataContext.Purchases != null)
                {
                    purchases = _dataContext.Purchases
                        .Include(p => p.AppUser)
                        .Include(p => p.Artwork)
                        .ThenInclude(a => a.AppUser)
                        .ToList();
                    return purchases;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return purchases;
        }
    }
}