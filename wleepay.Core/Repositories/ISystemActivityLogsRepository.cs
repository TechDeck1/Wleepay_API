using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;

namespace wleepay.Core.Repositories
{
    public interface ISystemActivityLogsRepository : IRepository<SystemActivityLog>
    {
        Task<ActivityResponseResource> GetAllActivityLogs(int pageIndex, int pageSize);
        Task<ActivityResponseResource> GetActivityLogsById(string userId , int pageIndex, int pageSize);
        Task<ActivityResponseResource> FilterLogsByEmail(string email, int index, int size);
        Task<SystemActivityLog> AddActivityLog(SystemActivityLog systemActivityLog);
        Task<bool> DeleteLogsById(int id);




    }
}
