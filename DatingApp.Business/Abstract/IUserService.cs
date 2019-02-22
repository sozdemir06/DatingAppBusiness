using System.Threading.Tasks;
using DatingApp.Business.Mappings.AutoMapper.Dtos;
using DatingApp.Entities.Concrete;

namespace DatingApp.Business.Abstract
{
    public interface IUserService
    {
         Task<UserForDetailedDto> Register(UserForRegisterDto userForRegisterDto);
         Task<UserForDetailedDto> Login(UserForLoginDto userForLoginDto);
         
    }
}