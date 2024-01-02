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
        Task<IList<SystemActivityLog>> GetActivityLogsById(string userId);
        Task<IList<SystemActivityLog>> FilterLogsByEmail(string email);
        Task<bool> DeleteLogsById(int id);




    }
}
