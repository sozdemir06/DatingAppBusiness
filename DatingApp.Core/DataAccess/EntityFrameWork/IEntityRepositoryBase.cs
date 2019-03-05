using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DatingApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Core.DataAccess.EntityFrameWork
{
    public class IEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()
    {
        public async Task<TEntity> Add(TEntity entity)
        {
             using(var context=new TContext())
             {
                 var addedEntity=context.Entry(entity);
                     addedEntity.State=EntityState.Added;
                     await context.SaveChangesAsync();
                     return entity; 
             }
        }

        public async Task<bool> Delete(TEntity Entity)
        {
           using(var context=new TContext())
           {
               var deletedEntity=context.Entry(Entity);
                    deletedEntity.State=EntityState.Deleted;
                return await context.SaveChangesAsync()>0;
           }
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
             using(var context=new TContext())
             {
                 return await context.Set<TEntity>().SingleOrDefaultAsync(filter);
             }
        }

        public async Task<IEnumerable<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using(var context=new TContext())
            {
                return filter==null
               ? await context.Set<TEntity>().ToListAsync()
               : await context.Set<TEntity>().Where(filter).ToListAsync();
            }
        }

        public async Task<bool> Update(TEntity entity)
        {
             using(var context=new TContext())
           {
               var updatedEntity=context.Entry(entity);
                    updatedEntity.State=EntityState.Modified;
                return await context.SaveChangesAsync()>0;
           }
        }
    }
}