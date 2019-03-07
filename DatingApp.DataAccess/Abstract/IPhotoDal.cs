using System.Threading.Tasks;
using DatingApp.Core.DataAccess;
using DatingApp.Entities.Concrete;

namespace DatingApp.DataAccess.Abstract
{
    public interface IPhotoDal:IEntityRepository<Photo>
    {
         Task<Photo> GetMainPhotoForUser(int userId);
    }
}