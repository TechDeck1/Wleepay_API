using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace wleepay.Services
{
    public class SystemActivityLogService : ISystemActivityLogsService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public SystemActivityLogService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<SystemActivityLog> AddSystemLogs(SystemActivityLog systemActivityLog)
        {
            var systemActivityResponse = await _unitOfWork.SystemActivity.AddActivityLog(systemActivityLog);
            return systemActivityResponse;       
        }

        public async Task<bool> DeleteLogsById(int id)
        {
            var result = await _unitOfWork.SystemActivity.DeleteLogsById(id);
            return result;
        }

        public async Task<ActivityResponseResource> FilterLogsByEmail(string email, int index, int size)
        {
            var filteredLogs = await _unitOfWork.SystemActivity.FilterLogsByEmail(email, index, size);
            return filteredLogs;
        }

        public async Task<ActivityResponseResource> GetAllSystemById(string userId, int pageIndex = 0, int pageSize = 10)
        {
            var logs = await _unitOfWork.SystemActivity.GetActivityLogsById(userId,pageIndex,pageSize);
            return logs;
        }

        public async Task<ActivityResponseResource> GetAllSystemLogs(int pageIndex=0, int pageSize = 10)
        {
            var logs = await _unitOfWork.SystemActivity.GetAllActivityLogs(pageIndex,pageSize);
            return logs;
        }

       
    }
}
