using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Presentation.Extensions;

namespace Presentation.SignalR
{
	public class PresenceHub : Hub
	{
        private readonly PresenceTracker _tracker;

        public PresenceHub(PresenceTracker tracker)
        {
            _tracker = tracker;
        }
        [Authorize]
        public override async Task OnConnectedAsync()
        {
            var isOnline = await _tracker.UserConnected(Context.User.GetEmailAddress(), Context.ConnectionId);
            if (isOnline)
                await Clients.Others.SendAsync("UserIsOnline", Context.User.GetEmailAddress());

            var currentUsers = await _tracker.GetOnlineUsers();
            await Clients.Caller.SendAsync("GetOnlineUsers", currentUsers);
        }
		

		[Authorize]
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var isOffline = await _tracker.UserDisconnected(Context.User.GetEmailAddress(), Context.ConnectionId);

            if (isOffline) await Clients.Others.SendAsync("UserIsOffline", Context.User.GetEmailAddress());

            await base.OnDisconnectedAsync(exception);
        }
    }
}
