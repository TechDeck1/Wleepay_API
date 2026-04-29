using System.ComponentModel.DataAnnotations;

namespace wleepay.API.Resources
{
    public class ChangeEmailRequest
    {
        [Required]
        [EmailAddress]
        public string NewEmail { get; set; }
    }
}
