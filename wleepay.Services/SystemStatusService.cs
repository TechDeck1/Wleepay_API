using MimeKit.Encodings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace wleepay.Services
{
    public class SystemStatusService : ISystemStatusService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SystemStatusService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }


        public async Task<SystemStatus> AddSystemStatus(SystemStatus status)
        {
            await _unitOfWork.SystemStatus.AddAsync(status);
            return status;
        }
    }
}
