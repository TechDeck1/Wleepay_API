using MimeKit.Encodings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.Enums;
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


        public async Task<SystemStatus> AddUpdateSystemStatus(SystemStatus status)
        {
            await _unitOfWork.SystemStatus.AddUpdateSystemStatus(status);
            return status;
        }

        public async Task<SystemStatus> GetSystemStatus()
        {
           var result=  await _unitOfWork.SystemStatus.GetSystemStatus();
           return result;
        }

        public async Task<SystemStatus> GetSystemStatusById(int id)
        {
            var result = await _unitOfWork.SystemStatus.GetSystemStatusById(id);
            return result;
        }
    }
}
