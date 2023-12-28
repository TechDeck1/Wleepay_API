using DevExpress.Data.ODataLinq.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Data.Context;
using System.Linq;


namespace wleepay.Data.Repositories
{
    public class SystemActivityLogsRepository : Repository<SystemActivityLog>, ISystemActivityLogsRepository
    {

        public SystemActivityLogsRepository(WPContext context): base(context) { }

        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }

        public async Task<IList<SystemActivityLog>> GetActivityLogsById(string userId)
        {
            var logs = await WPContext.SystemActivities.Where(user => user.UserId == userId).ToListAsync();
            return logs;
        }

        public async Task<IList<SystemActivityLog>> GetAllActivityLogs()
        {
            var logs = await WPContext.SystemActivities.ToListAsync();
            return logs;
        }


    }
}
