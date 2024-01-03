using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;

namespace wleepay.Core.Services
{
    public interface ISystemActivityLogsService
    {
        Task<SystemActivityLog> AddSystemLogs(SystemActivityLog systemActivityLog);
        Task<ActivityResponseResource> GetAllSystemLogs(int pageIndex, int pageSize);
        Task<IList<SystemActivityLog>> GetAllSystemById(string userId);
        Task<ActivityResponseResource> FilterLogsByEmail(string email, int index, int size);
        Task<bool> DeleteLogsById(int id);




    }
}
