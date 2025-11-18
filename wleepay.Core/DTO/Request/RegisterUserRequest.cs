using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace wleepay.Core.DTO.Request
{
    public class RegisterUserRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailId { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public int CountyId { get; set; }

        [Required]
        public string Gender { get; set; }

        public string Pin { get; set; }
    }
}
