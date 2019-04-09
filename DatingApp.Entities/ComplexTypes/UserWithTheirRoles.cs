using System.Collections;
using System.Collections.Generic;

namespace DatingApp.Entities.ComplexTypes
{
    public class UserWithTheirRoles
    {
        public int UserId { get; set; }
        public int UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}