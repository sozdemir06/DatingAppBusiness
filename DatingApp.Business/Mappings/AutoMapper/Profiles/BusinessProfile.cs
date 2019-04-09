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
            CreateMap<MessageForCreationDto,Message>().ReverseMap();
             
                
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
            CreateMap<User,UserWithRolesDto>()
                    .ForMember(dest=>dest.UserRoles,opt=>{
                        opt.MapFrom(src=>src.UserRoles.Select(x=>x.Role));
                    });
            CreateMap<Message,MessageToReturnDto>()
                        .ForMember(m=>m.SenderPhotoUrl,opt=>opt.MapFrom(u=>u.Sender.Photos.FirstOrDefault(p=>p.IsMain).Url))
                        .ForMember(m=>m.RecipientPhotoUrl,opt=>opt.MapFrom(u=>u.Recipient.Photos.FirstOrDefault(p=>p.IsMain).Url));

        }
    }
}