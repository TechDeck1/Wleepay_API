using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Services
{
    public interface ISystemActivityLogsService
    {
        Task<SystemActivityLog> AddSystemLogs(SystemActivityLog systemActivityLog);
        Task<IList<SystemActivityLog>> GetAllSystemLogs();
        Task<IList<SystemActivityLog>> GetAllSystemById(string userId);


    }
}
