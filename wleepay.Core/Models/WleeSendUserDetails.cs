using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace wleepay.Core.Models
{
    public class WleeSendUserDetails
    {
        [Key]
        //[ForeignKey("WleeSendUser")]
        public int Id { get; set; }
        [ForeignKey("WleeSendUser")]
        public string? UserId { get; set; }
        public string? PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Image { get; set; }
        public string? AccountNumber { get; set; }
        //public string

        public int? Status { get; set; }
        public int? FailedLoginAttempt { get; set; }
        public bool? IsLoggedIn { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string? Pin { get; set; }

        public virtual County? County { get; set; }
        public virtual WleeSendUser? WleeSendUser { get; set; }
    }
}
