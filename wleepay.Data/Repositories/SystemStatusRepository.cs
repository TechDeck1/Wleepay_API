using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Data.Context;

namespace wleepay.Data.Repositories
{
    public class SystemStatusRepository : Repository<SystemStatus>, ISystemStatusRepository
    {
        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }
        public SystemStatusRepository(WPContext context) : base(context) { }

        public async Task<SystemStatus> GetSystemStatus()
        {
            var result = await WPContext.SystemStatus.OrderByDescending(x=>x.CreatedDate).FirstOrDefaultAsync();
            return result;
        }

        public async Task<SystemStatus> GetSystemStatusById(int id)
        {
            var result = await WPContext.SystemStatus.Where(x=> x.Id == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<SystemStatus> AddUpdateSystemStatus(SystemStatus systemStatus)
        {
            if (systemStatus.Id <= 0)
            {
                await WPContext.SystemStatus.AddAsync(systemStatus);
                WPContext.SaveChanges();
            }
            else
            {
                var result = await WPContext.SystemStatus.Where(x => x.Id == systemStatus.Id).FirstOrDefaultAsync();
                result.Status = systemStatus.Status;
                WPContext.SaveChanges();

            }
            return systemStatus;
        }
    }
}
