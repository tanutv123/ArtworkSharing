using BusinessObject.DTOs;
using BusinessObject.Entities;

namespace Repository
{
	public interface ICommissionRepository
	{
		Task<Commission> GetArtistCommission(int id);
		Task<List<CommissionRequestHistoryDTO>> GetCommissionRequestHistory(int audienceId);
		Task<List<CommissionRequestHistoryDTO>> GetCommissionRequestHistoryForArtist(int artistId, string statusFilter);
		Task<List<CommissionRequestHistoryAdminDTO>> GetAllCommissionRequestHistory();

        Task<CommissionRequestHistoryDTO> GetSingleCommissionRequestHistory(int requestId);
		Task<bool> CheckArtistRegisterCommission(int id);
		Task AcceptCommissionRequest(int id, int actualPrice);
		Task NotAcceptCommissionRequest(int id, string notAcceptReason);
		Task DoneCommissionRequest(int id);
		Task RequestProgressImage(int id);
		Task AddCommission(Commission commission);
		Task AddCommissionRequest(CommissionRequest commissionRequest);
		Task AddCommissionImage(CommissionImage commissionImage);
		Task ResendCommission(CommissionResendDTO resend);
		Task<CommissionRequestHistoryAdminDTO> GetSingleCommissionRequestHistoryAdmin(int id);
		Task EditCommissionImage(CommissionImage image);
		Task Delete(CommissionImage image);
		Task<CommissionImage> GetCommissionImage(int imageId, int userId);

    }
}
