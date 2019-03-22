using System.Threading.Tasks;
using DatingApp.Entities.Concrete;

namespace DatingApp.Business.Abstract
{
    public interface ILikeService
    {
         Task<Like> AddLike(int userId,int recipientId);
    }
}