using System.Threading.Tasks;
using DatingApp.Core.DataAccess.EntityFrameWork;
using DatingApp.DataAccess.Abstract;
using DatingApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : IEntityRepositoryBase<User, DataContext>, IUserDal
    {
       

        public async Task<bool> UserExist(string email)
        {
            using(var context=new DataContext())
            {
                var findUserByEmail=await context.Users.FirstOrDefaultAsync(u=>u.Email==email);
                if(findUserByEmail==null)
                {
                    return false;
                }
                return true;
            }
        }
    }
}