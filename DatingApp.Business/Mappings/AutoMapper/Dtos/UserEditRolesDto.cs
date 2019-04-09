namespace DatingApp.Business.Mappings.AutoMapper.Dtos
{
    public class UserEditRolesDto
    {
        public int UserId { get; set; }
        public int[] RoleId { get; set; }
    }
}