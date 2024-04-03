using FluentValidation;
using wleepay.API.Resources;

namespace wleepay.API.Validators
{
    public class SetPasswordValidators : AbstractValidator<SetPinResources>
    {
        public SetPasswordValidators()
        {
            RuleFor(a => a.Pin)
                .NotEmpty()
                .NotNull()
                .MinimumLength(14)
                .MaximumLength(30);

            RuleFor(a => a.ConfirmPin)
                .NotNull()
                .NotEmpty()
                .MinimumLength(14)
                .MaximumLength(30);

            RuleFor(a => a.Email)
                .NotEmpty()
                .NotNull();

        }
    }
    
}
