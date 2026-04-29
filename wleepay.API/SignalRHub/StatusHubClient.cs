using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Management;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.Models;

namespace wleepay.API.SignalRHub
{
    public class StatusHubClient : Hub
    {
        private readonly IUnitOfWork _unitOfWork;
        public StatusHubClient(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task SendStatusToUser(SystemStatus systemStatus)
        {
            var result = await _unitOfWork.SystemStatus.GetSystemStatus();
            await Clients.All.SendAsync("sendStatusNotification", result);
        }

        public async Task SendNotification(string message)
        {
            await Clients.All.SendAsync("PortalNotification", message);
        }

    }
}
