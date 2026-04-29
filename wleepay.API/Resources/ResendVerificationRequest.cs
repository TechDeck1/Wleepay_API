using System.ComponentModel.DataAnnotations;

namespace wleepay.API.Resources
{
    public class ResendVerificationRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
