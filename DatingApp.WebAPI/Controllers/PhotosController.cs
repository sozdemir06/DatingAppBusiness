using System.Threading.Tasks;
using DatingApp.Business.Abstract;
using DatingApp.Business.Mappings.AutoMapper.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.WebAPI.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoService photoService;
        public PhotosController(IPhotoService photoService)
        {
            this.photoService = photoService;

        }

     [HttpPost]
     public async Task<IActionResult> AddPhotoForUser(int userId,[FromForm]PhotoForCreationDto photoForCreationDto)
     {
         var addPhoto=await photoService.AddPhotoForUser(userId,photoForCreationDto);
         return Ok(addPhoto);
     }

     [HttpPost("{photoId}/setMain")]
     public async Task<IActionResult> SetMainPhoto(int userId,int photoId)
     {
         await photoService.SetMainPhoto(userId,photoId);
         return Ok();
     }


    }
}