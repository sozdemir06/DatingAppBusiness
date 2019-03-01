using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Core.DataAccess.EntityFrameWork;
using DatingApp.DataAccess.Abstract;
using DatingApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : IEntityRepositoryBase<User, DataContext>, IUserDal
    {
        public async Task<IEnumerable<User>> GetUsersWithPhotos()
        {
            using(var context=new DataContext())
            {
                var users=await context.Users.Include(p=>p.Photos).ToListAsync();
                return users;
            }
        }

        public async Task<User> GetUserWithPhotos(int userId)
        {
            using(var context=new DataContext())
            {
                var user=await context.Users.Include(p=>p.Photos).FirstOrDefaultAsync(u=>u.Id==userId);
                return user;
            }
        }

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