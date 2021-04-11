using System.Text.RegularExpressions;
using Core.Constants;
using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator :AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty().WithMessage(Messages.CanNotBeBlank);
            RuleFor(u => u.FirstName).MinimumLength(2);
            
            
            RuleFor(u => u.LastName).NotEmpty().WithMessage(Messages.CanNotBeBlank);
            RuleFor(u => u.LastName).MinimumLength(2);

            RuleFor(u => u.Email).NotEmpty().WithMessage(Messages.CanNotBeBlank);
            RuleFor(u => u.Email).EmailAddress().WithMessage(Messages.InvalidEmailAddress);

           
        }
        
       
        
    }
}