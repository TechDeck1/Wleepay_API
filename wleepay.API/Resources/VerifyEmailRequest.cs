using System.ComponentModel.DataAnnotations;

namespace wleepay.API.Resources
{
    public class VerifyEmailRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
