using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace wleepay.Core.DTO.Request
{
    public class UserLoginRequest
    {
        //public bool IsLoggedIn { get; set; }
        [Required]
        [MaxLength(10)]
        public string PhoneNo { get; set; }
        [Required]
        public int PIN { get; set; }
    }
}
