using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DatingApp.Core.Entities;

namespace DatingApp.Core.DataAccess
{
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
         Task<IEnumerable<T>> GetList(Expression<Func<T,bool>> filter=null);
         Task<T> Get(Expression<Func<T,bool>> filter);
         Task<bool> Update(T entity);
         Task<bool> Delete(T Entity);
         Task<bool> Add(T entity);
    }
}