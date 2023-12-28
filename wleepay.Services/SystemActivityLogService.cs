using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace wleepay.Services
{
    public class SystemActivityLogService : ISystemActivityLogsService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public SystemActivityLogService(IUnitOfWork unitOfWork, ITransactionService transactionService)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<SystemActivityLog> AddSystemLogs(SystemActivityLog systemActivityLog)
        {
            systemActivityLog.CreatedDate = DateTime.Now;
            await _unitOfWork.SystemActivity.AddAsync(systemActivityLog);
            _unitOfWork.CommitAsync();
            return systemActivityLog;        }

        public async Task<IList<SystemActivityLog>> GetAllSystemById(string userId)
        {
            var logs = await _unitOfWork.SystemActivity.GetActivityLogsById(userId);
            return logs;
        }

        public async Task<IList<SystemActivityLog>> GetAllSystemLogs(int pageIndex=0, int pageSize = 10)
        {
            var logs = await _unitOfWork.SystemActivity.GetAllActivityLogs(pageIndex,pageSize);
            return logs;
        }
    }
}
