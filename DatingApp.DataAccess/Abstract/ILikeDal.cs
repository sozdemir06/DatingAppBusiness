using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DatingApp.Core.DataAccess;
using DatingApp.Entities.Concrete;

namespace DatingApp.DataAccess.Abstract
{
    public interface ILikeDal : IEntityRepository<Like>
    {
       
    }
}