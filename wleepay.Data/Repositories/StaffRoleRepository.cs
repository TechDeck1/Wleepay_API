using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Data.Context;

namespace wleepay.Data.Repositories
{
    public class StaffRoleRepository: Repository<StaffRoles>, IStaffRolesRepository
    { 
        public StaffRoleRepository(WPContext context) :base(context) { }

        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }

}
}
