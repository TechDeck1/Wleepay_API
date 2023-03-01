using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.Models
{
    public class ApplicationRole : IdentityRole<string>
    {
        public IList<ApplicationUserRole> UserRoles { get; set; }
    }
}
