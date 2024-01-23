using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Data.Context;

namespace wleepay.Data.Repositories
{
    public class UsersStaffRoleRepository: Repository<UsersStaffRole>, IUsersStaffRoleRepository
    {
        public UsersStaffRoleRepository(WPContext context) : base(context) { }
    }
}
