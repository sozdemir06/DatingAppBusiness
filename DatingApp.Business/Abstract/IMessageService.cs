using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Business.Mappings.AutoMapper.Dtos;
using DatingApp.Core.Utilities.Helpers.AppHeaderHelper;
using Microsoft.AspNetCore.Http;

namespace DatingApp.Business.Abstract
{
    public interface IMessageService
    {
        
        Task<MessageToReturnDto> CreateMessage(int userId,MessageForCreationDto messageForCreationDto);
        Task<IEnumerable<MessageToReturnDto>> GetMessagesForUser(HttpResponse response,int userId,MessageParams messageParams);
        Task<IEnumerable<MessageToReturnDto>> GetMessageThread(int userId,int recipientId);
        Task DeleteMessage(int messageId,int userId);
    }
}