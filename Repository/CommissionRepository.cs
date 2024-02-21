using BusinessObject.Entities;
using DataAccess.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public class CommissionRepository : ICommissionRepository
	{
		private readonly CommissionManagement _commissionManagement;

		public CommissionRepository(CommissionManagement commissionManagement)
        {
			_commissionManagement = commissionManagement;
		}

		public async Task AddCommission(Commission commission)
		{
			await _commissionManagement.Add(commission);
		}

		public async Task<bool> CheckArtistRegisterCommission(int id)
		{
			return await _commissionManagement.CheckArtistRegisterCommission(id);
		}

		public async Task<Commission> GetArtistCommission(int id)
		{
			return await _commissionManagement.GetArtistCommissionAsync(id);
		}
	}
}
