using Microsoft.AspNetCore.SignalR;
using wleepay.Core.Models;
using wleepay.Core;
using System.Threading.Tasks;


namespace wleepay.API.Hub
{
    public class TicketsNotificationHubClient : Hub<ITicketsNotificationHubClient>
    {
            private readonly IUnitOfWork _unitOfWork;
            public TicketsNotificationHubClient(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }

            public async Task GetLatestNotificationForTickets(string userId)
            {
                var result = await _unitOfWork.CustomerTickets.GetCustomerTicketsByUserId(userId);
                await Clients.All.GetLatestNotificationForTickets(result);
            }
        
    }
}
