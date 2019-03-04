using DatingApp.Business.Mappings.AutoMapper.Dtos;
using FluentValidation;

namespace DatingApp.Business.ValidationRules.FluentValidation
{
    public class UserForUpdateDtoValidator:AbstractValidator<UserForUpdateDto>
    {
        public UserForUpdateDtoValidator()
        {
            RuleFor(x=>x.Introduction).NotEmpty().WithMessage("Introduction can'be Empty.!");
            RuleFor(x=>x.LookingFor).NotEmpty().WithMessage("LookingFor Can't be empty.!");
            RuleFor(x=>x.Interests).NotEmpty().WithMessage("Interests can't be mepty.!");
            RuleFor(x=>x.City).NotEmpty().WithMessage("City can't be empty.!");
            RuleFor(x=>x.City).MaximumLength(50).WithMessage("City name can't be grater than 50 characters.!");
            RuleFor(x=>x.Country).NotEmpty().WithMessage("Country can't be empty.!");
            RuleFor(x=>x.City).MaximumLength(50).WithMessage("Country name can't be grater than 50 characters.!");
        }
    }
}