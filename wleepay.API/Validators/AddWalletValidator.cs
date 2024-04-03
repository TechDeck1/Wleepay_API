using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wleepay.API.Resources;

namespace wleepay.API.Validators
{
    public class AddWalletValidator : AbstractValidator<AddWalletResource>
    {
        public AddWalletValidator()
        {

            RuleFor(a => a.CurrencyId)
             .NotNull()
             .NotEmpty();

            RuleFor(a => a.Type)
            .NotNull()
            .NotEmpty();

            RuleFor(a => a.UserId)
            .NotNull()
            .NotEmpty();
        }
    }
}
