using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Business.Mappings.AutoMapper.Dtos;
using DatingApp.Entities.Concrete;

namespace DatingApp.Business.Abstract
{
    public interface IUserService
    {
         Task<UserForDetailedDto> Register(UserForRegisterDto userForRegisterDto);
         Task<UserForReturnTokenDto> Login(UserForLoginDto userForLoginDto);
         Task<IEnumerable<UserForListDto>> GetUSersWithPhotos();
         Task<UserForDetailedDto> GetUser(int userId);
         
    }
}