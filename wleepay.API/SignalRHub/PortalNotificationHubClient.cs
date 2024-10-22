using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace wleepay.API.SignalRHub
{
    public class PortalNotificationHubClient : Hub
    {
        public async Task SendNotification(string message)
        {
            await Clients.All.SendAsync("PortalNotification", message);
        }
    }
}
