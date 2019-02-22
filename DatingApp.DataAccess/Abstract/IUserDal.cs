using System.Threading.Tasks;
using DatingApp.Core.DataAccess;
using DatingApp.Entities.Concrete;

namespace DatingApp.DataAccess.Abstract
{
    public interface IUserDal:IEntityRepository<User>
    {
        Task<bool> UserExist(string email);
    }
}