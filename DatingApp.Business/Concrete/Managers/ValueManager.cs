using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Business.Abstract;
using DatingApp.DataAccess.Abstract;
using DatingApp.Entities.Concrete;

namespace DatingApp.Business.Concrete.Managers
{
    public class ValueManager : IValueService
    {
        private readonly IValueDal valueDal;

        public ValueManager(IValueDal valueDal)
        {
            this.valueDal = valueDal;
        }
        public async Task<IEnumerable<Value>> GetAllValues()
        {
            return await valueDal.GetList();
        }
    }
}