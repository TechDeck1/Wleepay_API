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
    public class SmileIdentityJobRepository : Repository<SmileIdentityJob>, ISmileIdentityJobRepository
    {
        public SmileIdentityJobRepository(DbContext context):base(context)
        {

        }

        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }

        public async Task<SmileIdentityJob> GetJobByUserId(string applicationUserId)
        {
            return await WPContext.SmileIdentityJobs.FirstOrDefaultAsync(s => s.ApplicationUserId == applicationUserId);
        }
    }
}
