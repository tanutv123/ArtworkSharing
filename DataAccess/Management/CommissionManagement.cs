using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessObject.DTOs;
using BusinessObject.Entities;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Management
{
	public class CommissionManagement
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public CommissionManagement(DataContext context, IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
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

		public async Task AddCommissionRequest(CommissionRequest commissionRequest)
		{
			try
			{
				if (commissionRequest == null)
				{
					throw new Exception("Invalid request");
				}
				else
				{
					var pendingStatus = await _context.CommissionStatus
						.SingleOrDefaultAsync(x => x.Description == "Pending");
					commissionRequest.CommissionStatusId = pendingStatus.Id;
					await _context.CommissionRequests.AddAsync(commissionRequest);
					await _context.SaveChangesAsync();
				}
			}
			catch (Exception ex) 
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<List<CommissionRequestHistoryDTO>> GetCommissionRequestHistory(int audienceId)
		{
			List<CommissionRequestHistoryDTO> result = null;

			try
			{
				result = await _context.CommissionRequests.Where(x => x.SenderId == audienceId).ProjectTo<CommissionRequestHistoryDTO>(_mapper.ConfigurationProvider).ToListAsync();
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return result;
		}

		public async Task<List<CommissionRequestHistoryDTO>> GetCommissionRequestHistoryForArtist(int artistId, string statusFilter)
		{
			List<CommissionRequestHistoryDTO> result = null;

			try
			{
				if(string.IsNullOrEmpty(statusFilter))
				{
					result = await _context.CommissionRequests.Where(x => x.ReceiverId == artistId).ProjectTo<CommissionRequestHistoryDTO>(_mapper.ConfigurationProvider).ToListAsync();
				} 
				else
				{
					var statusToBeFiltered = await _context.CommissionStatus.Where(x => x.Description == statusFilter).SingleOrDefaultAsync();
					result = await _context.CommissionRequests.Where(x => x.ReceiverId == artistId && x.CommissionStatusId == statusToBeFiltered.Id).ProjectTo<CommissionRequestHistoryDTO>(_mapper.ConfigurationProvider).ToListAsync();
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return result;
		}

		public async Task<CommissionRequestHistoryDTO> GetSingleCommissionRequest(int id)
		{
			CommissionRequestHistoryDTO result = null;

			try
			{
				result = await _context.CommissionRequests.Where(x => x.Id == id).ProjectTo<CommissionRequestHistoryDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return result;
		}

		public async Task ChangeCommissionRequestStatusToAccept(int id)
		{
			try
			{
				var commission = await _context.CommissionRequests.FindAsync(id);
				if(commission != null)
				{
					var currentStatus = await _context.CommissionStatus.AsNoTracking().SingleOrDefaultAsync(x => x.Id == commission.CommissionStatusId);
					if(currentStatus.Description == "Pending")
					{
						var acceptStatus = await _context.CommissionStatus.AsNoTracking().SingleOrDefaultAsync(x => x.Description == "Accepted");
						commission.CommissionStatusId = acceptStatus.Id;
						await _context.SaveChangesAsync();
					}
					else
					{
						throw new Exception("An exception occurred while changing the commission status");
					}
				}
				else
				{
					throw new Exception("Commission Not Found");
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task ChangeCommissionRequestStatusToNotAccept(int id)
		{
			try
			{
				var commission = await _context.CommissionRequests.FindAsync(id);
				if (commission != null)
				{
					var currentStatus = await _context.CommissionStatus.AsNoTracking().SingleOrDefaultAsync(x => x.Id == commission.CommissionStatusId);
					if (currentStatus.Description == "Pending")
					{
						var acceptStatus = await _context.CommissionStatus.AsNoTracking().SingleOrDefaultAsync(x => x.Description == "Not Accepted");
						commission.CommissionStatusId = acceptStatus.Id;
						await _context.SaveChangesAsync();
					}
					else
					{
						throw new Exception("An exception occurred while changing the commission status");
					}
				}
				else
				{
					throw new Exception("Commission Not Found");
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task ChangeCommissionRequestStatusToDone(int id)
		{
			try
			{
				var commission = await _context.CommissionRequests.FindAsync(id);
				if (commission != null)
				{
					var currentStatus = await _context.CommissionStatus.AsNoTracking().SingleOrDefaultAsync(x => x.Id == commission.CommissionStatusId);
					if (currentStatus.Description == "In Progress")
					{
						var doneStatus = await _context.CommissionStatus.AsNoTracking().SingleOrDefaultAsync(x => x.Description == "Done");
						commission.CommissionStatusId = doneStatus.Id;
						await _context.SaveChangesAsync();
					}
					else
					{
						throw new Exception("An exception occurred while changing the commission status");
					}
				}
				else
				{
					throw new Exception("Commission Not Found");
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task AddCommissionImage(CommissionImage commissionImage)
		{
			try
			{
				var commission = await _context.CommissionRequests.SingleOrDefaultAsync(x => x.Id == commissionImage.CommissionRequestId);
				if(commission != null)
				{
					var pendingStatus = await _context.CommissionStatus.Where(x => x.Description == "Accepted").SingleOrDefaultAsync();
					if(commission.CommissionStatusId == pendingStatus.Id)
					{
						var inProgressStatus = await _context.CommissionStatus.Where(x => x.Description == "In Progress").SingleOrDefaultAsync();
						commission.CommissionStatusId = inProgressStatus.Id;
					}
					var isMainImage = await _context.CommissionImages.Where(x => x.CommissionRequestId == commissionImage.CommissionRequestId && x.isMain == true).SingleOrDefaultAsync();
					if(isMainImage != null)
					{
						isMainImage.isMain = false;
					}
					_context.CommissionImages.Add(commissionImage);
					await _context.SaveChangesAsync();
				}
				else
				{
					throw new Exception("Commission not found!");
				}
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

	}
}
