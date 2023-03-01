using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace wleepay.Core.DTO.Request
{
    public class KYCStatusDTO
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DOB { get; set; } 
        [Required]
        public string Gender { get; set; }
        [Required]
        public string PhoneNo { get; set; }
        [Required]
        public int County { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public int DocType { get; set; }
        [Required]
        public string DocImg { get; set; }
        
        public string AccountType { get; set; }
    }
}
