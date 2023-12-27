using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Data.Context;

namespace wleepay.Data.Repositories
{
    public class SystemActivityLogsRepository : Repository<SystemActivityLog>, ISystemActivityLogsRepository
    {

        public SystemActivityLogsRepository(WPContext context): base(context) { }

        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }

        public async Task<IList<SystemActivityLog>> GetAllActivityLogs()
        {
            var tokenStorage = await WPContext.SystemActivities.ToListAsync();
            return tokenStorage;
        }
    }
}
