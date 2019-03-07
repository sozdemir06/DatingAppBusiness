using System.Linq;
using AutoMapper;
using DatingApp.Business.Extensions;
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
            CreateMap<UserForUpdateDto,User>();
            CreateMap<PhotoForCreationDto,Photo>();
             
                
            //Domain To Api Resource
            CreateMap<User,UserForListDto>()
                    .ForMember(dest=>dest.PhotoUrl,opt=>{
                        opt.MapFrom(src=>src.Photos.FirstOrDefault(p=>p.IsMain).Url);
                    })
                    .ForMember(dest=>dest.Age,opt=>{
                        opt.MapFrom(d=>d.DateOfBirth.Calculate());
                    });
            CreateMap<User,UserForDetailedDto>()
                    .ForMember(dest=>dest.PhotoUrl,opt=>{
                        opt.MapFrom(src=>src.Photos.FirstOrDefault(p=>p.IsMain).Url);
                    })
                    .ForMember(dest=>dest.Age,opt=>{
                        opt.MapFrom(d=>d.DateOfBirth.Calculate());
                    });
            CreateMap<User,UserForReturnTokenDto>()
                    .ForMember(dest=>dest.PhotoUrl,opt=>{
                        opt.MapFrom(src=>src.Photos.FirstOrDefault(p=>p.IsMain).Url);
                    });    
            CreateMap<Photo,PhotosForDetailedDto>();
            CreateMap<Photo,PhotosForReturnDto>();

        }
    }
}