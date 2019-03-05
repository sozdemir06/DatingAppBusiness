using DatingApp.Core.DataAccess.EntityFrameWork;
using DatingApp.DataAccess.Abstract;
using DatingApp.Entities.Concrete;

namespace DatingApp.DataAccess.Concrete.EntityFramework
{
    public class EfPhotoDal:IEntityRepositoryBase<Photo,DataContext>,IPhotoDal
    {
        
    }
}