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
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public PurchaseManagement(DataContext context, IMapper mapper)
        {
            _dataContext = context;
            _mapper = mapper;
        }
        

        public void AddPurchase(Purchase purchase)
        {
            try
            {
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
            List<Purchase> purchases = new List<Purchase>();
            try
            {
                if (_dataContext != null && _dataContext.Purchases != null)
                {
                    purchases = _dataContext.Purchases
                        .Include(p => p.AppUser)
                        .Include(p => p.Artwork)
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