using Microsoft.AspNetCore.SignalR;
using wleepay.Core.Models;
using wleepay.Core;
using System.Threading.Tasks;

namespace wleepay.API.SignalRHub
{
    public class TicketsNotificationHubClient : Hub
    {
        private readonly IUnitOfWork _unitOfWork;
        public TicketsNotificationHubClient(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task GetLatestNotificationForTickets(string userId)
        {
            var result = await _unitOfWork.CustomerTickets.GetNewCustomerTicketsByUserId(userId);
            await Clients.All.SendAsync("ReceiveTicketNotifications", result);

        }

    }
}
