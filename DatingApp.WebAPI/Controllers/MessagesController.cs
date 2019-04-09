using System.Threading.Tasks;
using DatingApp.Business.Abstract;
using DatingApp.Business.Mappings.AutoMapper.Dtos;
using DatingApp.Core.Utilities.Helpers.AppHeaderHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.WebAPI.Controllers
{
    [Authorize(Roles="Member,Admin")]
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService messageService;
        public MessagesController(IMessageService messageService)
        {
            this.messageService = messageService;

        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int userId,[FromBody]MessageForCreationDto messageForCreationDto)
        {
            var messageToCreate=await messageService.CreateMessage(userId,messageForCreationDto);
            return Ok(messageToCreate);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessagesForUser(int userId,[FromQuery]MessageParams messageParams)
        {
            var messagesForUser=await messageService.GetMessagesForUser(Response,userId,messageParams);
            return Ok(messagesForUser);
        }

        [HttpGet("thread/{recipientId}")]
        public async Task<IActionResult> GetMessageThread(int userId,int recipientId)
        {
            var messageThreads=await messageService.GetMessageThread(userId,recipientId);
            return Ok(messageThreads);
        }

        [HttpDelete("{messageId}")]
        public async Task<IActionResult> DeleteMessage(int messageId,int userId)
        {
            await messageService.DeleteMessage(messageId,userId);
            return Ok();
        }



    }
}