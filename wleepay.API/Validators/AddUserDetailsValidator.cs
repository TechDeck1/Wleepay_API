using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wleepay.API.Resources;

namespace wleepay.API.Validators
{
    public class AddUserDetailsValidator: AbstractValidator<AddUserDetailsResource>
    {
        public AddUserDetailsValidator(string role)
        {
            RuleFor(a => a.FirstName)
                .NotEmpty()
                .NotNull();

            RuleFor(a => a.PhoneNumber)
                .NotEmpty()
                .NotNull()
                .MinimumLength(10)
                .MaximumLength(13);

            RuleFor(a => a.CountyId)
                .NotEmpty()
                .GreaterThan(0)
                .NotNull();

            RuleFor(a => a.KYCStatusId)
                .NotEmpty()
                .GreaterThan(0)
                .NotNull();

            RuleFor(a => a.Gender)
                .NotEmpty()
                .NotNull();

            RuleFor(a => a.KYCLevel)
                .NotEmpty()
                .GreaterThan(0)
                .NotNull();

            if (role == "Staff")
            {

                RuleFor(a => a.Pin.ToString())
                   .NotNull()
                   .NotEmpty()
                   .MinimumLength(12)
                   .MaximumLength(50);
            }
           
        }
    }
}
