using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;
using wleepay.Core.Repositories;

namespace wleepay.Data.Repositories
{
    public class SystemStatusRepository : Repository<SystemStatus>, ISystemStatusRepository
    {
        public SystemStatusRepository(DbContext context) : base(context)
        {
        }
    }
}
