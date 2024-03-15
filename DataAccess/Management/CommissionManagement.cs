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
		private static CommissionManagement instance = null;
		private static readonly object instanceLock = new object();

		public CommissionManagement() { }

		public static CommissionManagement Instance
		{
			get
			{
				lock (instanceLock)
				{
					if (instance == null)
					{
						instance = new CommissionManagement();
					}
					return instance;
				}
			}
		}

		public IQueryable<CommissionRequest> GetAllCommissionsAsQueryable()
		{
			var _context = new DataContext();
			return _context.CommissionRequests.AsQueryable();
		}
		public IQueryable<CommissionStatus> GetAllCommissionStatusAsQueryable()
		{
			var _context = new DataContext();
			return _context.CommissionStatus.AsQueryable();
		}

		public async Task<Commission> GetArtistCommissionAsync(int id)
		{
			Commission commission = null;

			try
			{
				var _context = new DataContext();
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
				var _context = new DataContext();
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
				var _context = new DataContext();
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
				var _context = new DataContext();
				if (commissionRequest == null)
				{
					throw new Exception("Invalid request");
				}
				else
				{
					var pendingStatus = await _context.CommissionStatus
						.SingleOrDefaultAsync(x => x.Description == "Pending");
					commissionRequest.CommissionStatusId = pendingStatus.Id;
					commissionRequest.RequestDate = DateTime.UtcNow;
					await _context.CommissionRequests.AddAsync(commissionRequest);
					await _context.SaveChangesAsync();
				}
			}
			catch (Exception ex) 
			{
				throw new Exception(ex.Message);
			}
		}
		public async Task ChangeCommissionRequestStatusToAccept(int id, int actualPrice)
		{
			try
			{
				var _context = new DataContext();
				var commission = await _context.CommissionRequests.FindAsync(id);
				if(commission != null)
				{
					var currentStatus = await _context.CommissionStatus.AsNoTracking().SingleOrDefaultAsync(x => x.Id == commission.CommissionStatusId);
					if(currentStatus.Description == "Pending")
					{
						var acceptStatus = await _context.CommissionStatus.AsNoTracking().SingleOrDefaultAsync(x => x.Description == "Accepted");
						if (commission.ActualPrice > commission.MaxPrice)
						{
							throw new Exception("Your price exceeds the audience's budget");
						}
						commission.CommissionStatusId = acceptStatus.Id;
						commission.ActualPrice = actualPrice;
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

		public async Task ChangeCommissionRequestStatusToNotAccept(int id, string notAcceptReason)
		{
			try
			{
				var _context = new DataContext();
				var commission = await _context.CommissionRequests.FindAsync(id);
				if (commission != null)
				{
					var currentStatus = await _context.CommissionStatus.AsNoTracking().SingleOrDefaultAsync(x => x.Id == commission.CommissionStatusId);
					if (currentStatus.Description == "Pending")
					{
						var acceptStatus = await _context.CommissionStatus.AsNoTracking().SingleOrDefaultAsync(x => x.Description == "Not Accepted");
						commission.CommissionStatusId = acceptStatus.Id;
						commission.NotAcceptedReason = notAcceptReason;
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
				var _context = new DataContext();
				var commission = await _context.CommissionRequests.FindAsync(id);
				if (commission != null)
				{
					var currentStatus = await _context.CommissionStatus.AsNoTracking().SingleOrDefaultAsync(x => x.Id == commission.CommissionStatusId);
					var checkImages = await _context.CommissionImages.AnyAsync(x => x.CommissionRequestId == id);
					if (currentStatus.Description == "In Progress" && checkImages)
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
				var _context = new DataContext();
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
					commission.Status = 0;
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

		public async Task RequestProgressImage(int id)
		{
			try
			{
				var _context = new DataContext();
				var commission = await _context.CommissionRequests.SingleOrDefaultAsync(x => x.Id == id);
				if(commission != null)
				{
					commission.Status = 1;
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

		public async Task ResendCommissionRequest(CommissionResendDTO commissionResendDTO)
		{
			try
			{
				var _context = new DataContext();
				var commission = await _context.CommissionRequests.FindAsync(commissionResendDTO.Id);
				if(commission != null)
				{
					commission.RequestDescription = commissionResendDTO.RequestDescription;
					commission.MinPrice = commissionResendDTO.MinPrice;
					commission.MaxPrice = commissionResendDTO.MaxPrice;
					commission.ActualPrice = 0;
					commission.DueDate = commissionResendDTO.DueDate;
					commission.NotAcceptedReason = string.Empty;
					var pendingStatus = await _context.CommissionStatus.SingleOrDefaultAsync(x => x.Description == "Pending");
					commission.CommissionStatusId = pendingStatus.Id;
					await _context.SaveChangesAsync();
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task EditCommissionImage(CommissionImage image)
		{
			try
			{
				var _context = new DataContext();
				var _image = await _context.CommissionImages.FindAsync(image.Id);
				if (_image == null) throw new Exception("Image not found");
				_image.Description = image.Description;
				_image.Url = image.Url;
				_image.PublicId = image.PublicId;
				await _context.SaveChangesAsync();
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public async Task Delete(CommissionImage image)
		{
			try
			{
				var _context = new DataContext();
				var _image = await _context.CommissionImages.FindAsync(image.Id);
				if (_image == null) throw new Exception("Image not found");
				_context.CommissionImages.Remove(_image);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<CommissionImage> GetCommissionImage(int imageId, int userId)
		{
			CommissionImage image = null;
			try
			{
				var _context = new DataContext();
				image = await _context.CommissionImages.FindAsync(imageId);
				if (!await _context.CommissionRequests.AnyAsync(x => x.Id == image.CommissionRequestId && x.ReceiverId == userId))
				{
					image = null;
				}
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return image;
		}

	}
}
