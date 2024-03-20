using FluentValidation;
using wleepay.API.Resources;
using wleepay.Core.Models;

namespace wleepay.API.Validators
{
    public class SystemStatusValidator : AbstractValidator<SystemStatusResource>
    {
        public SystemStatusValidator()
        {
            RuleFor(a => a.Description)
                .NotEmpty()
                .NotNull()
                .MinimumLength(1)
                .MaximumLength(500);

            RuleFor(a => a.Reason)
                .NotNull()
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(500);

            RuleFor(a => a.Status)
                .NotEmpty();
               
        }
    }
    
}
