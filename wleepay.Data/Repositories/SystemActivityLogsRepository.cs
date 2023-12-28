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
using MailKit.Search;


namespace wleepay.Data.Repositories
{
    public class SystemActivityLogsRepository : Repository<SystemActivityLog>, ISystemActivityLogsRepository
    {

        public SystemActivityLogsRepository(WPContext context): base(context) { }

        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }

        public async Task<bool> DeleteLogsById(int id)
        {
            var entityToDelete = WPContext.SystemActivities.Find(id); // Retrieve the entity by its primary key
            if (entityToDelete != null)
            {
                WPContext.SystemActivities.Remove(entityToDelete); // Mark the entity for deletion
                WPContext.SaveChanges(); // Save changes to delete the entity from the database
            }
            return true;
        }

        public  async Task<IList<SystemActivityLog>> FilterLogsByEmail(string email)
        {
            var filteredData = await WPContext.SystemActivities
                .Where(entity => entity.Email.Contains(email)).ToListAsync();
            return filteredData;
                
        }

        public async Task<IList<SystemActivityLog>> GetActivityLogsById(string userId)
        {
            var logs = await WPContext.SystemActivities.Where(user => user.UserId == userId).ToListAsync();
            return logs;
        }

        public async Task<IList<SystemActivityLog>> GetAllActivityLogs(int pageIndex = 0, int pageSize = 10)
        {
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;
            var logs = await WPContext.SystemActivities
                .Skip(skipCount)
                .Take(takeCount)
                .ToListAsync();
            return logs;
        }

      
    }
}
