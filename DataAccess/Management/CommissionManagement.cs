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

		public async Task<bool> CheckArtistRegisterCommission(int id)
		{
			bool result = false;

			try
			{
				result = await _context.Commissions.AnyAsync(x => x.AppUserId == id);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return result;
		}

		public async Task Add(Commission commission)
		{
			try
			{
				var _commission = await _context.Commissions.AnyAsync(x => x.Id == commission.Id);
				if(!_commission)
				{
					var checkeCommission = await _context.Commissions.AnyAsync(x => x.AppUserId == commission.AppUserId);
					if (checkeCommission)
					{
						throw new Exception("You already have your commission");
					}
					else
					{
						_context.Commissions.Add(commission);
						await _context.SaveChangesAsync();
					}
				}
				else
				{
					throw new Exception("Commission already exists");
				}
				
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
    }
}
