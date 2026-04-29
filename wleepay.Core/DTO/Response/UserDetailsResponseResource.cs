using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Core.DTO.Response
{
    public class UserDetailsResponseResource
    {
        public int TotalCount { get; set; }
       // public virtual DateTime? LastTransactionDate { get; set; }
        public List<ApplicationUser> UserDetails { get; set; }
    }


    public class TicketsResponse
    {
        public int TotalCount { get; set; }

        public List<UserDetails> UserDetails { get; set; }
    }


    public class UserDetails
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountNumber { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
