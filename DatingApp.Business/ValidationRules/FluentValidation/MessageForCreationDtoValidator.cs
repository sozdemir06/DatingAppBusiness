using DatingApp.Business.Mappings.AutoMapper.Dtos;
using FluentValidation;

namespace DatingApp.Business.ValidationRules.FluentValidation
{
    public class MessageForCreationDtoValidator:AbstractValidator<MessageForCreationDto>
    {
        public MessageForCreationDtoValidator()
        {
            RuleFor(x=>x.Content).NotEmpty().WithMessage("Message content can't be empty.!!");
            RuleFor(x=>x.Content).MaximumLength(140).WithMessage("Message Content must have be maximum 140 characters.!!");
            //RuleFor(x=>x.SenderId).NotEmpty().WithName("Sender can't be recognized.!!");
            //RuleFor(x=>x.RecipientId).NotEmpty().WithName("Recipient can't be recognized.!!");
        }
    }
}