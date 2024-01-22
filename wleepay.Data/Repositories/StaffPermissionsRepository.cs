using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Data.Context;

namespace wleepay.Data.Repositories
{
    public class StaffPermissionsRepository: Repository<StaffPermissions>, IStaffPermissionsRepository
    {
        public StaffPermissionsRepository(WPContext context) :base(context){ }
    }
}
