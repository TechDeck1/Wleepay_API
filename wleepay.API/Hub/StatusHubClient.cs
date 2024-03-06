using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.Models;

namespace wleepay.API.Hub
{
    public class StatusHubClient : Hub<IStatusHubClient>
    {
        private readonly IUnitOfWork _unitOfWork;
        public StatusHubClient(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task SendStatusToUser()
        {
            var result = await _unitOfWork.SystemStatus.GetSystemStatus();
            await Clients.All.SendStatusToUser(result);
        }
    }
}
