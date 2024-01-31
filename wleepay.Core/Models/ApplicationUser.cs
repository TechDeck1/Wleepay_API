using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace wleepay.Core.Models
{
    public class ApplicationUser : IdentityUser<string>
    {
        public ApplicationUser()
        {
            KYCdocuments = new Collection<KYCdocuments>();
            Wallets = new Collection<Wallet>();
            SmileIdentityJobs = new Collection<SmileIdentityJob>();
            UsersStaffRole=new Collection<UsersStaffRole>();

        }

        public IList<ApplicationUserRole> UserRoles { get; set; }
        public virtual UserDetail UserDetail { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<KYCdocuments> KYCdocuments { get; set; }
        public virtual ICollection<Wallet> Wallets { get; set; }
        public virtual ICollection<SmileIdentityJob> SmileIdentityJobs { get; set; }
        public virtual ICollection<UsersStaffRole> UsersStaffRole { get; set; }


    }
}
