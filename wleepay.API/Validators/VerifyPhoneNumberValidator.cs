using FluentValidation;
using wleepay.API.Resources;

namespace wleepay.API.Validators
{
   
        public class VerifyPhoneNumberValidator : AbstractValidator<SystemAdminVerificationModel>
        {
            public VerifyPhoneNumberValidator()
            {


            RuleFor(a => a.PhoneNumber)
                .NotEmpty()
                .WithMessage("phone number should not be empty")
                .NotNull()
                .WithMessage("phone number should not be null")
                .MinimumLength(10)
                .WithMessage("phone number minimum length should be 10.")
                .MaximumLength(13)
                .WithMessage("phone number maximum length should be 13");


            RuleFor(a => a.Code)
                .NotEmpty()
                .WithMessage("code should not be null or empty")
                .NotNull()
                .WithMessage("code should not be null or empty")
                .Length(4)
                .WithMessage("code length should be 4");

            }
        }
    
}
