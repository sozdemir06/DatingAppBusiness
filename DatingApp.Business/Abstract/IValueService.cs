using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Entities.Concrete;

namespace DatingApp.Business.Abstract
{
    public interface IValueService
    {
         Task<IEnumerable<Value>> GetAllValues();
    }
}