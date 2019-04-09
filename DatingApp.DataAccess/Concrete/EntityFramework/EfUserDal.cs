using System;
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

        public async Task<PagedList<User>> GetUserLikers(UserParams userParams)
        {
            using(var context=new DataContext())
            {
                var users=context.Users
                            .Include(p=>p.Photos)
                            .Include(u=>u.Likers)
                            .Include(u=>u.Likees)
                            .AsQueryable();
                if(userParams.Likers)
                {
                    users=users.Where(i=>i.Likers.Any(u=>u.LikerId==userParams.UserId));
                }
                if(userParams.Likees)
                {
                    users=users.Where(i=>i.Likees.Any(u=>u.LikeeId==userParams.UserId));
                }
                
                return await PagedList<User>.CreateAsync(users,userParams.PageNumber,userParams.PageSize);
                
            }
        }

        public async Task<PagedList<User>> GetUsersWithPhotos(UserParams userParams)
        {
            using(var context=new DataContext())
            {
                var users=context.Users.Include(p=>p.Photos).AsQueryable();
                    users=users.Where(u=>u.Id!=userParams.UserId);
                    users=users.Where(u=>u.Gender==userParams.Gender);
                    if(userParams.MinAge!=18 || userParams.MaxAge!=99)
                    {
                        var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                        var maxDob = DateTime.Today.AddYears(-userParams.MinAge);

                        users=users.Where(u=>u.DateOfBirth>=minDob && u.DateOfBirth<=maxDob);
                    }
               

                return await PagedList<User>.CreateAsync(users,userParams.PageNumber,userParams.PageSize);
            }
        }

        public async Task<IEnumerable<User>> GetUsersWithRoles()
        {
            using(var context=new DataContext())
            {
                var usersWithRoles=await context.Users.Include(ur=>ur.UserRoles)
                                            .ThenInclude(r=>r.Role)
                                            .ToListAsync();
                return usersWithRoles;
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

        public async Task<User> GetUserWithRoles(int userId)
        {
            using(var context=new DataContext())
            {
                var getuserWithRoles=await context.Users.Include(ur=>ur.UserRoles).ThenInclude(p=>p.Role).FirstOrDefaultAsync(u=>u.Id==userId);
                return getuserWithRoles;
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