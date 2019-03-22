using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.Core.DataAccess.EntityFrameWork;
using DatingApp.DataAccess.Abstract;
using DatingApp.DataAccess.Concrete.EntityFramework;
using DatingApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.DataAccess.Concrete
{
    public class EfLikeDal : IEntityRepositoryBase<Like, DataContext>, ILikeDal
    {
        
    }
}