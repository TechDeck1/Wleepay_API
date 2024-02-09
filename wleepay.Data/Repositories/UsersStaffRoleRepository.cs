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
    public class UsersStaffRoleRepository: Repository<UsersStaffRole>, IUsersStaffRoleRepository
    {
        public UsersStaffRoleRepository(WPContext context) : base(context) { }

        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }
        public async Task<UsersStaffRole> AddUserStaffRole(UsersStaffRole usersStaffRole)
        {
            if (usersStaffRole.UserId == null)
            {
                await WPContext.UsersStaffRole.AddAsync(usersStaffRole);
                WPContext.SaveChanges();
            }
            else
            {
                var result = WPContext.UsersStaffRole.Where(x=>x.UserId == usersStaffRole.UserId).FirstOrDefault();
                result.UpdatedDate = DateTime.Now;
                result.StaffRoleId = usersStaffRole.StaffRoleId;
                WPContext.SaveChanges();

            }
            return usersStaffRole;
        }
    }
}
