using AutoMapper;
using DatingApp.Business.Mappings.AutoMapper.Dtos;
using DatingApp.Entities.Concrete;

namespace DatingApp.Business.Mappings.AutoMapper.Profiles
{
    public class BusinessProfile:Profile
    {
        public BusinessProfile()
        {
            //Api Resource To Domain
            CreateMap<UserForRegisterDto,User>();

            //Domain To Api Resource
            CreateMap<User,UserForDetailedDto>();

        }
    }
}