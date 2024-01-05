using System.ComponentModel.DataAnnotations;

namespace wleepay.API.Resources
{
    public class ResetPasswordResource
    {
       
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        
    }

    public class CheckTokenExpirationResource
    {
        public string? Email { get; set; }
        public string? Token { get; set; }

    }
}
