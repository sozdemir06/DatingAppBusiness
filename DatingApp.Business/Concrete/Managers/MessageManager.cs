using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.Business.Abstract;
using DatingApp.Business.Mappings.AutoMapper.Dtos;
using DatingApp.Business.ValidationRules.FluentValidation;
using DatingApp.Core.Aspects.PostSharp.ValidationAspects;
using DatingApp.Core.Utilities.Helpers.AppHeaderHelper;
using DatingApp.DataAccess.Abstract;
using DatingApp.Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace DatingApp.Business.Concrete.Managers
{

    public class MessageManager : IMessageService
    {
        private readonly IMessageDal messageDal;
        private readonly IUserDal userDal;

        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        public MessageManager(IMessageDal messageDal, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserDal userDal)
        {
            this.userDal = userDal;
            this.messageDal = messageDal;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;


        }

        [FluentValidationAspect(typeof(MessageForCreationDtoValidator))]
        public async Task<MessageToReturnDto> CreateMessage(int userId, MessageForCreationDto messageForCreationDto)
        {
            var sender=await userDal.GetUserWithPhotos(userId);
            if (sender.Id != int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                throw new Exception("UnAuthorized.!!");
            }
            messageForCreationDto.SenderId = userId;

            var recipient = await userDal.Get(u=>u.Id==messageForCreationDto.RecipientId);
            if(recipient==null)
            {
                throw new Exception("Could not find user.!!");
            }
            var message=mapper.Map<Message>(messageForCreationDto);
            var addMesage=await messageDal.Add(message);
            if(addMesage==null)
            {
                throw new Exception("Creating the messsage failed on save.!! ");
            }

            var getAddedMessage=await messageDal.GetAddedMessageWithUserInfo(addMesage.Id);
            return mapper.Map<MessageToReturnDto>(getAddedMessage);
        }

        public async Task DeleteMessage(int messageId, int userId)
        {
            var currentUserId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                if (userId != currentUserId)
                {
                    throw new Exception("UnAuthorized.!!");
                }
                var getDeletedMessageFromRepo=await messageDal.Get(m=>m.Id==messageId);
                if(getDeletedMessageFromRepo.SenderId==userId)
                {
                    getDeletedMessageFromRepo.SenderDeleted=true;
                }
                if(getDeletedMessageFromRepo.RecipientId==userId)
                {
                    getDeletedMessageFromRepo.RecipientDeleted=true;
                }
                if(!getDeletedMessageFromRepo.SenderDeleted && !getDeletedMessageFromRepo.RecipientDeleted)
                {
                    throw new Exception("Message sender deleted or recipient delete can not set the true.!!");
                    
                }
                var deleteMessage=await messageDal.Delete(getDeletedMessageFromRepo);
                if(!deleteMessage)
                {
                    throw new Exception("Message could'nt deleted.!!");
                }
        }

        public async Task<IEnumerable<MessageToReturnDto>> GetMessagesForUser(HttpResponse response,int userId,MessageParams messageParams)
        {
              var currentUserId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                if (userId != currentUserId)
                {
                    throw new Exception("UnAuthorized.!!");
                }
                messageParams.UserId=userId;

                var messages=await messageDal.GetMessagesForUser(messageParams);
                if(messages==null)
                {
                    throw new Exception("Messages not found.!!");
                }

                var messagesToreturn=mapper.Map<IEnumerable<MessageToReturnDto>>(messages);
                response.AddPagination(messages.CurrentPage,messages.PageSize, messages.TotalCount, messages.TotalPages); 
                return messagesToreturn;
               
        }

        public async Task<IEnumerable<MessageToReturnDto>> GetMessageThread(int userId, int recipientId)
        {
           var currentUserId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                if (userId != currentUserId)
                {
                    throw new Exception("UnAuthorized.!!");
                }
            
            var messageThread=await messageDal.GetMessageThread(userId,recipientId);
            if(messageThread==null)
            {
                throw new Exception("Message not found.!!");
            }

            var messageToreturn=mapper.Map<IEnumerable<MessageToReturnDto>>(messageThread);
            return messageToreturn;
        }

    }
}