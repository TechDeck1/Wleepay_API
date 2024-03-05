using System.Collections.Generic;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.API.Hub
{
    public interface IStatusHubClient
    {
        Task SendStatusToUser(SystemStatus systemStatus);

    }
}
