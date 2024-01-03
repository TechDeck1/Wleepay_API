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
using Microsoft.SqlServer.Management.Smo;
using wleepay.Core.DTO.Response;
using static DevExpress.XtraPrinting.Native.PageSizeInfo;
using Org.BouncyCastle.Utilities;

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

        public  async Task<ActivityResponseResource> FilterLogsByEmail(string email, int index, int size)
        {
            int takeCount = size;
            int skipCount = index * size;
            var filteredData = await WPContext.SystemActivities
                .Where(entity => entity.Email.Contains(email)).ToListAsync();
            var TotalCount = filteredData.Count();
            var activityLogs = filteredData.Skip(skipCount)
                             .Take(takeCount);

            ActivityResponseResource response = new ActivityResponseResource();
            response.ActivityLogs = activityLogs.OrderByDescending(x=>x.CreatedDate).ToList();
            response.TotalCount = TotalCount;


            return response;
            //return filteredData;
                
        }

        public async Task<ActivityResponseResource> GetActivityLogsById(string userId, int pageIndex = 0, int pageSize = 10)
        {
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;
            var logs = await WPContext.SystemActivities.Where(user => user.UserId == userId).ToListAsync();
            var TotalCount = logs.Count();
            var activityLogs = logs.Skip(skipCount)
                             .Take(takeCount);

            ActivityResponseResource response = new ActivityResponseResource();
            response.ActivityLogs = activityLogs.OrderByDescending(x => x.CreatedDate).ToList();
            response.TotalCount = TotalCount;


            return response;
        }

        public async Task<ActivityResponseResource> GetAllActivityLogs(int pageIndex = 0, int pageSize = 10)
        {
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;
            var logs = await WPContext.SystemActivities               
                .ToListAsync();
            var TotalCount=logs.Count();
            var activityLogs = logs.Skip(skipCount)
                             .Take(takeCount);

            ActivityResponseResource response = new ActivityResponseResource();
            response.ActivityLogs = activityLogs.OrderByDescending(x => x.CreatedDate).ToList();
            response.TotalCount = TotalCount;


            return response;

        }


    }
}
