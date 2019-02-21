using DatingApp.Entities.Concrete;
using FluentValidation;

namespace DatingApp.Business.ValidationRules.FluentValidation
{
    public class UserValidator:AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x=>x.UserName).NotEmpty().WithMessage("UserName Can't be empty");
            RuleFor(x=>x.UserName).MaximumLength(50).WithMessage("UserName Can't be more than 50 Character");
            RuleFor(x=>x.PasswordHash).NotEmpty().WithMessage("PasswordHash Can't be empty");
            RuleFor(x=>x.PasswordSalt).NotEmpty().WithMessage("PasswordSalt Can't be empty");
        }
    }
}