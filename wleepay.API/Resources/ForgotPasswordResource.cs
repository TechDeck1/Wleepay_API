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

    public class LinkForLoginSetupResource
    {
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? ClientURI { get; set; }

    }

    public class SetupLoginResources
    {
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string Answer { get; set; }
        public int SecurityQuestionId { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }

    }

    public class AccountExistVerificationResource
    {
        public string? Email { get; set; }
    }

    public class CompleteSignUpResourceModel
    {
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? ClientURI { get; set; }
    }

    public class SetPinResources
    {
        public string? Pin { get; set; }
        public string? ConfirmPin { get; set; }
        public string? Email { get; set; }

    }


}
