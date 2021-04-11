using System.Data;
using Core.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator :AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.DailyPrice).NotEmpty().WithMessage(Messages.CanNotBeBlank);
            RuleFor(c => c.DailyPrice).GreaterThan(50);
            
            
            RuleFor(c => c.Description).NotEmpty().WithMessage(Messages.CanNotBeBlank);
            RuleFor(c => c.Description).MinimumLength(5);
            
            RuleFor(c => c.ModelYear).NotEmpty().WithMessage(Messages.CanNotBeBlank);

                       
            RuleFor(c => c.BrandId).NotEmpty();
            RuleFor(c => c.ColorId).NotEmpty();
        }
    }
}