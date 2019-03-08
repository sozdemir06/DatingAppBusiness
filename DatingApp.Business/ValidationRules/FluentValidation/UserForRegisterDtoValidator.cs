using DatingApp.Business.Mappings.AutoMapper.Dtos;
using FluentValidation;

namespace DatingApp.Business.ValidationRules.FluentValidation
{
    public class UserForRegisterDtoValidator:AbstractValidator<UserForRegisterDto>
    {
        public UserForRegisterDtoValidator()
        {
            RuleFor(x=>x.Email).NotEmpty().WithMessage("Email address can't be empty.!!");
            RuleFor(x=>x.Email).EmailAddress().WithMessage("Incorrect email Adres.!!");
            RuleFor(x=>x.Email).MaximumLength(50).WithMessage("Email address can't exceed 50 characters.!!");
            RuleFor(x=>x.UserName).NotEmpty().WithMessage("Username Can't be empty.!!");
            RuleFor(x=>x.UserName).MaximumLength(50).WithMessage("UserName Can't exceed 50 characters.!!");
            RuleFor(x=>x.Password).NotEmpty().WithMessage("Password can't be empty");
            RuleFor(x=>x.Password).MinimumLength(4).MaximumLength(8).WithMessage("Password must be at least 4 and maximum 8 characters.!!");
            RuleFor(x=>x.KnownAs).NotEmpty().WithMessage("KnownAs can't be empty.!");
            RuleFor(x=>x.City).NotEmpty().WithMessage("City can't be empty.!");
            RuleFor(x=>x.Country).NotEmpty().WithMessage("Country can't be empty.!");
            RuleFor(x=>x.DateOfBirth).NotEmpty().WithMessage("DateofBirth can't be empty.!");
            
        }
        
    }
}