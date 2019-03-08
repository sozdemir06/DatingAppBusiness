using System.Threading.Tasks;
using DatingApp.Business.Mappings.AutoMapper.Dtos;

namespace DatingApp.Business.Abstract
{
    public interface IPhotoService
    {
         Task<PhotosForReturnDto> AddPhotoForUser(int userId,PhotoForCreationDto photoForCreationDto);
         Task SetMainPhoto(int userId,int photoId);
         Task DeletePhoto(int userId,int photoId);
    }
}