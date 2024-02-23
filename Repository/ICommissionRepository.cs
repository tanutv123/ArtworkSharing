﻿using BusinessObject.DTOs;
using BusinessObject.Entities;

namespace Repository
{
	public interface ICommissionRepository
	{
		Task<Commission> GetArtistCommission(int id);
		Task<List<CommissionRequestHistoryDTO>> GetCommissionRequestHistory(int audienceId);
		Task<List<CommissionRequestHistoryDTO>> GetCommissionRequestHistoryForArtist(int artistId, string statusFilter);
		Task<List<CommissionRequestHistoryDTO>> GetAllCommissionRequestHistory();

        Task<CommissionRequestHistoryDTO> GetSingleCommissionRequestHistory(int requestId);
		Task<bool> CheckArtistRegisterCommission(int id);
		Task AddCommission(Commission commission);
		Task AddCommissionRequest(CommissionRequest commissionRequest);
	}
}
