using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.API.Resources
{
    public class UserAccountResource
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public UserDetailResource UserDetail { get; set; }
        public IEnumerable<WalletResource> Wallets { get; set; }
        public AddressResource Address { get; set; }
        public ApplicationRole UserRoles { get; set; }
        public IEnumerable<UsersStaffRoleResource> UsersStaffRole { get; set; }

    }
}
