using System.IdentityModel.Tokens.Jwt;

namespace DatingApp.Business.Mappings.AutoMapper.Dtos
{
    public class UserForReturnTokenDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Token { get; set; }
        public string PhotoUrl { get; set; }
    }
}