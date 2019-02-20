using DatingApp.Core.Entities;

namespace DatingApp.Entities.Concrete
{
    public class Value:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}