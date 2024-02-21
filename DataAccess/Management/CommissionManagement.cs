using BusinessObject.Entities;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DataAccess.Management
{
	public class CommissionManagement
	{
		private readonly DataContext _context;

		public CommissionManagement(DataContext context)
        {
			_context = context;
		}

		public async Task<Commission> GetArtistCommissionAsync(int id)
		{
			Commission commission = null;

			try
			{
				commission = await _context.Commissions.SingleOrDefaultAsync(x => x.AppUserId == id);
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}

			return commission;
		}
    }
}
