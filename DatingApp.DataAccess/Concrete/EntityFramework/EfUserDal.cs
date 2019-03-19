using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.Core.DataAccess.EntityFrameWork;
using DatingApp.Core.Utilities.Helpers.AppHeaderHelper;
using DatingApp.DataAccess.Abstract;
using DatingApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : IEntityRepositoryBase<User, DataContext>, IUserDal
    {
        public async Task<User> CheckUserForLogin(string email)
        {
           using(var context=new DataContext())
           {
               return await context.Users.Include(p=>p.Photos).FirstOrDefaultAsync(u=>u.Email==email);
           }
        }

        public async Task<PagedList<User>> GetUsersWithPhotos(UserParams userParams)
        {
            using(var context=new DataContext())
            {
                var users=context.Users.Include(p=>p.Photos);
                return await PagedList<User>.CreateAsync(users,userParams.PageNumber,userParams.PageSize);
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