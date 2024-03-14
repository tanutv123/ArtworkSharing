using BusinessObject.DTOs;
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

		public async Task AcceptCommissionRequest(int id, int actualPrice)
		{
			await _commissionManagement.ChangeCommissionRequestStatusToAccept(id, actualPrice);
		}

		public async Task AddCommission(Commission commission)
		{
			await _commissionManagement.Add(commission);
		}

		public async Task AddCommissionImage(CommissionImage commissionImage)
		{
			await _commissionManagement.AddCommissionImage(commissionImage);
		}

		public async Task AddCommissionRequest(CommissionRequest commissionRequest)
		{
			await _commissionManagement.AddCommissionRequest(commissionRequest);
		}

		public async Task<bool> CheckArtistRegisterCommission(int id)
		{
			return await _commissionManagement.CheckArtistRegisterCommission(id);
		}

        public async Task<List<CommissionRequestHistoryAdminDTO>> GetAllCommissionRequestHistory()
        {
            return await _commissionManagement.GetAllCommissionRequestHistory();
        }

		public async Task DoneCommissionRequest(int id)
		{
			await _commissionManagement.ChangeCommissionRequestStatusToDone(id);
		}

		public async Task<Commission> GetArtistCommission(int id)
		{
			return await _commissionManagement.GetArtistCommissionAsync(id);
		}

		public async Task<List<CommissionRequestHistoryDTO>> GetCommissionRequestHistory(int audienceId)
		{
			return await _commissionManagement.GetCommissionRequestHistory(audienceId);
		}

		public async Task<List<CommissionRequestHistoryDTO>> GetCommissionRequestHistoryForArtist(int artistId, string statusFilter)
		{
			return await _commissionManagement.GetCommissionRequestHistoryForArtist(artistId, statusFilter);
		}

		public async Task<CommissionRequestHistoryDTO> GetSingleCommissionRequestHistory(int requestId)
        {
			return await _commissionManagement.GetSingleCommissionRequest(requestId);
        }

		public async Task NotAcceptCommissionRequest(int id, string notAcceptReason)
		{
			await _commissionManagement.ChangeCommissionRequestStatusToNotAccept(id, notAcceptReason);
		}

		public async Task RequestProgressImage(int id)
		{
			await _commissionManagement.RequestProgressImage(id);
		}

		public async Task ResendCommission(CommissionResendDTO resend)
		{
			await _commissionManagement.ResendCommissionRequest(resend);
		}
		public async Task<CommissionRequestHistoryAdminDTO> GetSingleCommissionRequestHistoryAdmin(int id)
		{
			return await _commissionManagement.GetSingleCommissionRequestAdmin(id);
		}

    }
}
