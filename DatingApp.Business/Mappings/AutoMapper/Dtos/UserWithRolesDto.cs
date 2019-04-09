using System.Collections.Generic;
using DatingApp.Entities.Concrete;

namespace DatingApp.Business.Mappings.AutoMapper.Dtos
{
    public class UserWithRolesDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public ICollection<UserRolesDto> UserRoles { get; set; }
        
    }
}