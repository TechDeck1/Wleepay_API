using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using wleepay.Core.Models;

namespace wleepay.Core.DTO.Request
{
    public class UserRegistrationDTO
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        [Required]
        public int PIN { get; set; }
        [Required]
        [MaxLength(10)]
        public string PhoneNo { get; set; }
        [Required]
        [EmailAddress]
        public string EmailID { get; set; }
        public DateTime? Dob { get; set; }
        [Required]
        public int Kycid { get; set; }
        public string Image { get; set; }
        [Required]
        public int CountyID { get; set; }
        public string DocumentNo { get; set; }
        public int DocumentID { get; set; }
        [Required]
        public string AccountType { get; set; }

        public string KYCImage { get; set; }
    }
}
