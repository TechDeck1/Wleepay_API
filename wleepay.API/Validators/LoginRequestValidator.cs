using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wleepay.API.Resources;

namespace wleepay.API.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(a => a.Email)
                .NotEmpty()
                .NotNull()
                .MaximumLength(13);

            RuleFor(a => a.Pin.ToString())
                .NotNull()
                .NotEmpty()
                .Length(4);
        }
    }
}
