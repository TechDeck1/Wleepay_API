using FluentValidation;
using wleepay.API.Resources;

namespace wleepay.API.Validators
{
    public class AddStaffDetailsValidator : AbstractValidator<AddStaffDetailsResource>
    {
        public AddStaffDetailsValidator()
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

            //RuleFor(a => a.KYCLevel)
            //    .NotEmpty()
            //    .GreaterThan(0)
            //    .NotNull();

            
        }
    }
}
