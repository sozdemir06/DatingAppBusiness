using System.Linq;
using System.Threading.Tasks;
using DatingApp.Core.DataAccess.EntityFrameWork;
using DatingApp.DataAccess.Abstract;
using DatingApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.DataAccess.Concrete.EntityFramework
{
    public class EfPhotoDal : IEntityRepositoryBase<Photo, DataContext>, IPhotoDal
    {
        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
           using(var context=new DataContext())
           {
               return await context.Photos.Where(u=>u.UserId==userId).FirstOrDefaultAsync(p=>p.IsMain);
           }
        }
    }
}