using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Repositories
{
    public interface ISystemStatusRepository : IRepository<SystemStatus>
    {
        Task<SystemStatus> AddUpdateSystemStatus(SystemStatus systemStatus);
        Task<SystemStatus> GetSystemStatus();
        Task<SystemStatus> GetSystemStatusById(int id);
    }
}
