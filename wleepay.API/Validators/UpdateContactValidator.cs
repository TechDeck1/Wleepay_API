using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wleepay.API.Resources;

namespace wleepay.API.Validators
{
    public class UpdateContactValidator : AbstractValidator<UpdateContactResource>
    {
        public UpdateContactValidator(bool isEmail = false)
        {
            if (isEmail)
            {
                RuleFor(a => a.EmailId)
                .NotEmpty()
                .NotNull();
            }
            else
            {
                RuleFor(a => a.PhoneNumber)
                .NotEmpty()
                .NotNull()
                .MinimumLength(10);
            }

            RuleFor(a => a.UserId)
                .NotNull()
                .NotEmpty();
        }
    }
}
