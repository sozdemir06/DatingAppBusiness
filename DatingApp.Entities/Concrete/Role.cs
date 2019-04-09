using System.Collections.Generic;
using DatingApp.Core.Entities;

namespace DatingApp.Entities.Concrete
{
    public class Role:IEntity
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        
    }
}