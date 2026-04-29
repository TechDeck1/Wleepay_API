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

            RuleFor(a => a.EmailId)
                .NotEmpty()
                .NotNull()
                .EmailAddress();

            RuleFor(a => a.KYCStatusId)
                .NotEmpty()
                .GreaterThan(0)
                .NotNull();

            RuleFor(a => a.KYCLevel)
                .NotEmpty()
                .GreaterThan(0)
                .NotNull();

            RuleFor(a => a.Pin)
                .NotEmpty()
                .NotNull();

            When(a => !string.IsNullOrWhiteSpace(a.Pin), () =>
            {
                RuleFor(a => a.ConfirmPin)
                    .NotEmpty()
                    .NotNull()
                    .Equal(a => a.Pin)
                    .WithMessage("Pin and Confirm Pin must match.");
            });

            if (role == "Staff")
            {
                RuleFor(a => a.Pin)
                    .NotNull()
                    .NotEmpty()
                    .MinimumLength(12)
                    .MaximumLength(50);
            }
            else
            {
                When(a => !string.IsNullOrWhiteSpace(a.Pin), () =>
                {
                    RuleFor(a => a.Pin)
                        .MinimumLength(4)
                        .MaximumLength(50);
                });
            }
           
        }
    }
}
