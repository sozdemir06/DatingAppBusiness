using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DatingApp.Core.DataAccess.EntityFrameWork;
using DatingApp.DataAccess.Abstract;
using DatingApp.Entities.Concrete;

namespace DatingApp.DataAccess.Concrete.EntityFramework
{
    public class EfUserRoleDal : IEntityRepositoryBase<UserRole, DataContext>, IUserRoleDal
    {
       
    }
}