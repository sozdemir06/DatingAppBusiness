using System.Threading.Tasks;
using DatingApp.Business.Abstract;
using DatingApp.Business.Mappings.AutoMapper.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterDto userForRegisterDto)
        {
            var registerUser=await userService.Register(userForRegisterDto);
            return Ok(registerUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserForLoginDto userForLoginDto)
        {
            var userToLogin=await userService.Login(userForLoginDto);
            return Ok(userToLogin);
        }
    }
}