using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Data.Context;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

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
            return await WPContext.SystemStatus.OrderByDescending(x=>x.CreatedDate).FirstOrDefaultAsync();
        }

        public async Task<SystemStatus> GetSystemStatusById(int id)
        {
            return await WPContext.SystemStatus.Where(x=> x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<SystemStatus> AddUpdateSystemStatus(SystemStatus systemStatus)
        {
            SystemStatus result = systemStatus;
            if (systemStatus.Id <= 0)
            {
                await WPContext.SystemStatus.AddAsync(systemStatus);
                WPContext.SaveChanges();
            }
            else
            {
                result = await WPContext.SystemStatus.Where(x => x.Id == systemStatus.Id).FirstOrDefaultAsync();
                result.Status = systemStatus.Status;
                WPContext.SaveChanges();

            }
            return result;
        }
    }
}
