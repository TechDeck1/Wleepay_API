using System.ComponentModel.DataAnnotations;

namespace wleepay.API.Resources
{
    public class ForgotPasswordResource
    {       
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? ClientURI { get; set; }
        
    }
}
