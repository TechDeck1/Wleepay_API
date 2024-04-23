using System.Collections.Generic;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.API.Hub
{

    public interface ITicketsNotificationHubClient
    {
        Task GetLatestNotificationForTickets(IEnumerable<CustomerTickets> customerTickets);
    }
}
