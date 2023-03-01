using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wleepay.API.Resources;

namespace wleepay.API.Validators
{
    public class AddKYCDocumentValidator : AbstractValidator<IEnumerable<KYCDocumentResource>>
    {
        public AddKYCDocumentValidator()
        {
            RuleFor(a => a.Select(s => s.DocumentImage))
                .NotEmpty()
                .NotNull();

            RuleFor(a => a.Select(s => s.DocumentNumber))
                .NotEmpty()
                .NotNull();

            RuleFor(a => a.Select(s => s.Type))
                .NotEmpty()
                .NotNull();

            RuleFor(a => a.Select(s => s.UserId))
                .NotEmpty()
                .NotNull();
        }
    }
}
