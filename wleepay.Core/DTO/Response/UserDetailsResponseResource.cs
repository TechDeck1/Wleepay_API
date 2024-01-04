using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Core.DTO.Response
{
    public class UserDetailsResponseResource
    {
        public int TotalCount { get; set; }

        public List<ApplicationUser> UserDetails { get; set; }
    }
}
