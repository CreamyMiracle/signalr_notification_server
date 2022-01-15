using Microsoft.AspNetCore.SignalR;

namespace NotificationServer.Hubs
{
    // POI 2
    public class NotificationHub : Hub
    {
        public async Task JoinGroup(string group)
        {
            await Groups.AddToGroupAsync(this.Context.ConnectionId, group);
        }
    }
}
