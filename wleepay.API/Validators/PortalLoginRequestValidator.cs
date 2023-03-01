using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wleepay.API.Resources;

namespace wleepay.API.Validators
{
    public class PortalLoginRequestValidator : AbstractValidator<PortalLoginRequest>
    {
        public PortalLoginRequestValidator()
        {
            RuleFor(a => a.Email)
                .NotEmpty()
                .NotNull();

            RuleFor(a => a.Password)
                .NotNull()
                .NotEmpty();
        }
    }
}
