using DatingApp.Core.Entities;

namespace DatingApp.Entities.Concrete
{
    public class UserRole:IEntity
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
    }
}