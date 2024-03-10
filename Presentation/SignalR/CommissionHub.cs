using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Presentation.Extensions;
using Repository;

namespace Presentation.SignalR
{
	public class CommissionHub : Hub
	{
		private readonly IHubContext<PresenceHub> _presenceHub;
		private readonly ICommissionRepository _commissionRepository;

		public CommissionHub(IHubContext<PresenceHub> presenceHub, ICommissionRepository commissionRepository)
        {
			_presenceHub = presenceHub;
			_commissionRepository = commissionRepository;
		}

		public async Task CommissionReceived(string artistEmailAddress)
		{
			var connections = await PresenceTracker.GetConnectionsForUser(artistEmailAddress);
			if (connections != null)
			{
				await _presenceHub.Clients.Clients(connections).SendAsync("CommissionReceived", Context.User.GetEmailAddress());
			}
		}

		public async Task RequestProgressImage(int commissionId)
		{
			var commission = await _commissionRepository.GetSingleCommissionRequestHistory(commissionId);
			if (commission != null)
			{
				var connections = await PresenceTracker.GetConnectionsForUser(commission.ReceiverEmail);
				if (connections != null)
				{
					await _presenceHub.Clients.Clients(connections).SendAsync("ProgressImageRequest", commissionId);
				}
			}
		}
		public async Task AcceptCommission(int commissionId)
		{
			var commission = await _commissionRepository.GetSingleCommissionRequestHistory(commissionId);
			if (commission != null)
			{
				var connections = await PresenceTracker.GetConnectionsForUser(commission.SenderEmail);
				if (connections != null)
				{
					await _presenceHub.Clients.Clients(connections).SendAsync("CommissionAccept", commission.ReceiverEmail, commissionId);
				}
			}
		}
		public async Task NotAcceptCommission(int commissionId)
		{
			var commission = await _commissionRepository.GetSingleCommissionRequestHistory(commissionId);
			if(commission != null)
			{
				var connections = await PresenceTracker.GetConnectionsForUser(commission.SenderEmail);
				if (connections != null)
				{
					await _presenceHub.Clients.Clients(connections).SendAsync("CommissionNotAccept", commission.ReceiverEmail, commissionId);
				}
			}
		}
		public async Task AddProgressImage(int commissionId)
		{
			var commission = await _commissionRepository.GetSingleCommissionRequestHistory(commissionId);
			if (commission != null)
			{
				var connections = await PresenceTracker.GetConnectionsForUser(commission.SenderEmail);
				if (connections != null)
				{
					await _presenceHub.Clients.Clients(connections).SendAsync("AddProgressImage", commission.ReceiverEmail, commissionId);
				}
			}
		}
		public async Task DoneCommission(int commissionId)
		{
			var commission = await _commissionRepository.GetSingleCommissionRequestHistory(commissionId);
			if (commission != null)
			{
				var connections = await PresenceTracker.GetConnectionsForUser(commission.SenderEmail);
				if (connections != null)
				{
					await _presenceHub.Clients.Clients(connections).SendAsync("DoneCommission", commission.ReceiverEmail, commissionId);
				}
			}
		}
	}
}
