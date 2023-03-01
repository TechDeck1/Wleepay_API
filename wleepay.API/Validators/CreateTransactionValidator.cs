using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wleepay.API.Resources;

namespace wleepay.API.Validators
{
    public class CreateTransactionValidator : AbstractValidator<CreateTransactionResource>
    {
        public CreateTransactionValidator()
        {

            RuleFor(a => a.Amount)
             .NotNull()
             .GreaterThan(0);

            RuleFor(a => a.FromWalletId)
                .NotNull()
                .NotEmpty();

            RuleFor(a => a.ToWalletId)
                .NotNull()
                .NotEmpty();

            RuleFor(a => a.TransferTypeId)
                    .NotNull()
                    .NotEmpty();
        }
    }
}
