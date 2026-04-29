using System.ComponentModel.DataAnnotations;

namespace wleepay.API.Resources
{
    public class ConfirmEmailChangeRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
