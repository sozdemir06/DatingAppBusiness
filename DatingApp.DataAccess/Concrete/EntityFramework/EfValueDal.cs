using DatingApp.Core.DataAccess.EntityFrameWork;
using DatingApp.DataAccess.Abstract;
using DatingApp.Entities.Concrete;

namespace DatingApp.DataAccess.Concrete.EntityFramework
{
    public class EfValueDal:IEntityRepositoryBase<Value,DataContext>,IValueDal
    {
        
    }
}