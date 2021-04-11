using Core.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.CompanyName).NotEmpty().WithMessage(Messages.CanNotBeBlank);
            RuleFor(c => c.CompanyName).MinimumLength(2);
            RuleFor(c => c.FirstName).NotEmpty();
            RuleFor(c => c.LastName).NotEmpty();


        }
    }
}