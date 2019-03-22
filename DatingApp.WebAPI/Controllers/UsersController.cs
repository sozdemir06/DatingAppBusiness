using System.Threading.Tasks;
using DatingApp.Business.Abstract;
using DatingApp.Business.Extensions;
using DatingApp.Business.Mappings.AutoMapper.Dtos;
using DatingApp.Core.Utilities.Helpers.AppHeaderHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILikeService likeService;
        public UsersController(IUserService userService, ILikeService likeService)
        {
            this.likeService = likeService;
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams)
        {
            var users = await userService.GetUSersWithPhotos(Response, userParams);
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await userService.GetUser(userId);
            return Ok(user);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody]UserForUpdateDto userForUpdateDto)
        {
            var updateUser = await userService.UpdateUser(userId, userForUpdateDto);
            return Ok(updateUser);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterDto userForRegisterDto)
        {
            var registerUser = await userService.Register(userForRegisterDto);
            return Ok(registerUser);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserForLoginDto userForLoginDto)
        {
            var userToLogin = await userService.Login(userForLoginDto);
            return Ok(userToLogin);
        }

        [HttpPost("{userId}/like/{recipientId}")]
        public async Task<IActionResult> AddLike(int userId, int recipientId)
        {
            var liked=await likeService.AddLike(userId,recipientId);
            return Ok(liked);
        }

        [HttpGet("getlikers")]
        public async Task<IActionResult> GetUserLikers([FromQuery]UserParams userParams)
        {
            var users=await userService.GetUserLikers(Response,userParams);
            return Ok(users);
        }
    }
}