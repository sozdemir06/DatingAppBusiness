using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Core.DataAccess;
using DatingApp.Entities.Concrete;

namespace DatingApp.DataAccess.Abstract
{
    public interface IUserDal:IEntityRepository<User>
    {
        Task<bool> UserExist(string email);
        Task<IEnumerable<User>> GetUsersWithPhotos();
        Task<User> GetUserWithPhotos(int userId);
        Task<User> CheckUserForLogin(string email);
    }
}