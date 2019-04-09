using DatingApp.Business.Mappings.AutoMapper.Dtos;
using FluentValidation;

namespace DatingApp.Business.ValidationRules.FluentValidation
{
    public class PhotoForCreationDtoValidator:AbstractValidator<PhotoForCreationDto>
    {
        public PhotoForCreationDtoValidator()
        {
            RuleFor(x=>x.Description).MaximumLength(140).WithMessage("Photo Description can't grater than 140 characters.!!");    
            //RuleFor(x=>x.Url).NotEmpty().WithMessage("Photo Url can't be empty.!!");
            //RuleFor(x=>x.PublicId).NotEmpty().WithMessage("Photo PublicId can't be empty.!!");
            
        }
    }
}