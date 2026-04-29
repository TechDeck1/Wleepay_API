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
                .WithMessage("amount cannot be null")
                .GreaterThan(0).WithMessage("amount should be greater than 0");

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
