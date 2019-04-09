using System.Threading.Tasks;
using DatingApp.Business.Abstract;
using DatingApp.Business.Extensions;
using DatingApp.Business.Mappings.AutoMapper.Dtos;
using DatingApp.Core.Utilities.Helpers.AppHeaderHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.WebAPI.Controllers
{
    
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

        [Authorize(Roles="Member,Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams)
        {
            var users = await userService.GetUSersWithPhotos(Response, userParams);
            return Ok(users);
        }

        [Authorize(Roles="Member,Admin")]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await userService.GetUser(userId);
            return Ok(user);
        }

        [Authorize(Roles="Member,Admin")]
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

        [Authorize(Roles="Member,Admin")]
        [HttpPost("{userId}/like/{recipientId}")]
        public async Task<IActionResult> AddLike(int userId, int recipientId)
        {
            var liked=await likeService.AddLike(userId,recipientId);
            return Ok(liked);
        }

        [Authorize(Roles="Member,Admin")]
        [HttpGet("getlikers")]
        public async Task<IActionResult> GetUserLikers([FromQuery]UserParams userParams)
        {
            var users=await userService.GetUserLikers(Response,userParams);
            return Ok(users);
        }

        [Authorize(Roles="Member,Admin")]
        [HttpGet("{userId}/roles")]
        public async Task<IActionResult> GetUserWithRoles(int userId)
        {
            var getuserWithRoles=await userService.GetUserWithRoles(userId);
            return Ok(getuserWithRoles);
        }


        [Authorize(Roles="Admin,Moderator")]
        [HttpGet("roles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var userWithroles=await userService.GetUsersWithRoles();
            return Ok(userWithroles);
        }

        [Authorize(Roles="Admin,Moderator")]
        [HttpPost("editRoles")]
        public async Task<IActionResult> EditUserRoles([FromBody]UserEditRolesDto userEditRolesDto)
        {
            var editUserRoles=await userService.EditUserRoles(userEditRolesDto);
            return Ok(editUserRoles);
        }
    }
}