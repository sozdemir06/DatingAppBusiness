using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Business.Extensions;
using DatingApp.Business.Mappings.AutoMapper.Dtos;
using DatingApp.Core.Utilities.Helpers.AppHeaderHelper;
using DatingApp.Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace DatingApp.Business.Abstract
{
    public interface IUserService
    {
         Task<UserForDetailedDto> Register(UserForRegisterDto userForRegisterDto);
         Task<UserForReturnTokenDto> Login(UserForLoginDto userForLoginDto);
         Task<IEnumerable<UserForListDto>> GetUSersWithPhotos(HttpResponse response,UserParams userParams);
         Task<UserForDetailedDto> GetUser(int userId);
         Task<UserForUpdateDto> UpdateUser(int userId,UserForUpdateDto userForUpdateDto);
         Task<IEnumerable<UserForListDto>> GetUserLikers(HttpResponse response,UserParams userParams);
         
    }
}