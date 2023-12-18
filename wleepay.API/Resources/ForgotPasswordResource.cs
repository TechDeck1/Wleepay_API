using System.ComponentModel.DataAnnotations;

namespace wleepay.API.Resources
{
    public class ForgotPasswordResource
    {       
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Answer { get; set; }
        public int SecurityQuestionId { get; set; }

        [Required]
        public string? ClientURI { get; set; }

        
    }
}
