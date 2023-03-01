using FluentValidation;
using wleepay.API.Resources;

namespace wleepay.API.Validators
{
    public class TaxRateRequestValidator : AbstractValidator<TaxRateResourceModel>
    {
        public TaxRateRequestValidator()
        {
            RuleFor(a => a.Id)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);

            RuleFor(a => a.AgentCommission)
                .NotEmpty()
                .NotNull();

            RuleFor(a => a.OrganizationFee)
                .NotEmpty()
                .NotNull();

            RuleFor(a => a.TaxAmount)
                .NotEmpty()
                .NotNull();
        }
    }
}
