using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessObject.DTOs;
using BusinessObject.Entities;
using DataAccess.Management;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
	public class CommissionRepository : ICommissionRepository
	{
		private readonly IMapper _mapper;

		public CommissionRepository(CommissionManagement commissionManagement, IMapper _mapper)
        {
			this._mapper = _mapper;
		}

		public async Task AcceptCommissionRequest(int id, int actualPrice)
		{
			await CommissionManagement.Instance.ChangeCommissionRequestStatusToAccept(id, actualPrice);
		}

		public async Task AddCommission(Commission commission)
		{
			await CommissionManagement.Instance.Add(commission);
		}

		public async Task AddCommissionImage(CommissionImage commissionImage)
		{
			await CommissionManagement.Instance.AddCommissionImage(commissionImage);
		}

		public async Task AddCommissionRequest(CommissionRequest commissionRequest)
		{
			await CommissionManagement.Instance.AddCommissionRequest(commissionRequest);
		}

		public async Task<bool> CheckArtistRegisterCommission(int id)
		{
			return await CommissionManagement.Instance.CheckArtistRegisterCommission(id);
		}

        public async Task<List<CommissionRequestHistoryAdminDTO>> GetAllCommissionRequestHistory()
        {
			List<CommissionRequestHistoryAdminDTO> result;
			try
			{
				result = await CommissionManagement.Instance.GetAllCommissionsAsQueryable().Include(c => c.CommissionImages).ProjectTo<CommissionRequestHistoryAdminDTO>(_mapper.ConfigurationProvider).ToListAsync();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
			return result;
        }

		public async Task DoneCommissionRequest(int id)
		{
			await CommissionManagement.Instance.ChangeCommissionRequestStatusToDone(id);
		}

		public async Task<Commission> GetArtistCommission(int id)
		{
			return await CommissionManagement.Instance.GetArtistCommissionAsync(id);
		}

		public async Task<List<CommissionRequestHistoryDTO>> GetCommissionRequestHistory(int audienceId)
		{
			var query = CommissionManagement.Instance.GetAllCommissionsAsQueryable();
			List<CommissionRequestHistoryDTO> result;
			try
			{
				result = await query.Where(x => x.SenderId == audienceId).ProjectTo<CommissionRequestHistoryDTO>(_mapper.ConfigurationProvider).ToListAsync();
			}
			catch (Exception ex)
			{
				throw new Exception();
			}
			return result;
		}

		public async Task<List<CommissionRequestHistoryDTO>> GetCommissionRequestHistoryForArtist(int artistId, string statusFilter)
		{
			List<CommissionRequestHistoryDTO> result;
			result = await CommissionManagement.Instance.GetAllCommissionsAsQueryable().Where(x => x.ReceiverId == artistId).ProjectTo<CommissionRequestHistoryDTO>(_mapper.ConfigurationProvider).ToListAsync();
			try
			{
				if (string.IsNullOrEmpty(statusFilter))
				{
					result = await CommissionManagement.Instance.GetAllCommissionsAsQueryable().Where(x => x.ReceiverId == artistId).ProjectTo<CommissionRequestHistoryDTO>(_mapper.ConfigurationProvider).ToListAsync();
				}
				else
				{
					var statusToBeFiltered = await CommissionManagement.Instance.GetAllCommissionStatusAsQueryable().Where(x => x.Description == statusFilter).SingleOrDefaultAsync();
					result = await CommissionManagement.Instance.GetAllCommissionsAsQueryable().Where(x => x.ReceiverId == artistId && x.CommissionStatusId == statusToBeFiltered.Id).ProjectTo<CommissionRequestHistoryDTO>(_mapper.ConfigurationProvider).ToListAsync();
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return result;
		}

		public async Task<CommissionRequestHistoryDTO> GetSingleCommissionRequestHistory(int requestId)
        {
			CommissionRequestHistoryDTO result = null;

			try
			{
				result = await CommissionManagement.Instance.GetAllCommissionsAsQueryable().Where(x => x.Id == requestId).Include(c => c.CommissionImages).ProjectTo<CommissionRequestHistoryDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return result;
        }

		public async Task NotAcceptCommissionRequest(int id, string notAcceptReason)
		{
			await CommissionManagement.Instance.ChangeCommissionRequestStatusToNotAccept(id, notAcceptReason);
		}

		public async Task RequestProgressImage(int id)
		{
			await CommissionManagement.Instance.RequestProgressImage(id);
		}

		public async Task ResendCommission(CommissionResendDTO resend)
		{
			await CommissionManagement.Instance.ResendCommissionRequest(resend);
		}
		public async Task<CommissionRequestHistoryAdminDTO> GetSingleCommissionRequestHistoryAdmin(int id)
		{
			CommissionRequestHistoryAdminDTO result;
			try
			{
				result = await CommissionManagement.Instance.GetAllCommissionsAsQueryable().Where(x => x.Id == id).Include(c => c.CommissionImages).ProjectTo<CommissionRequestHistoryAdminDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return result;
		}

		public async Task EditCommissionImage(CommissionImage image)
		{
			await CommissionManagement.Instance.EditCommissionImage(image);
		}

		public async Task<CommissionImage> GetCommissionImage(int imageId, int userId)
		{
			return await CommissionManagement.Instance.GetCommissionImage(imageId, userId);
		}

		public async Task Delete(CommissionImage image)
		{
			await CommissionManagement.Instance.Delete(image);
		}
	}
}
