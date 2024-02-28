using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Presentation.Extensions;

namespace Presentation.SignalR
{
	public class CommissionHub : Hub
	{
		private readonly IHubContext<PresenceHub> _presenceHub;

		public CommissionHub(IHubContext<PresenceHub> presenceHub)
        {
			_presenceHub = presenceHub;
		}

		public async Task CommissionReceived(string artistEmailAddress)
		{
			var connections = await PresenceTracker.GetConnectionsForUser(artistEmailAddress);
			if (connections != null)
			{
				await _presenceHub.Clients.Clients(connections).SendAsync("CommissionReceived", Context.User.GetEmailAddress());
			}
		}
	}
}
