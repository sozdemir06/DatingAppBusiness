using DatingApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.DataAccess.Concrete.EntityFramework.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x=>x.Id);

            builder.Property(x=>x.PasswordHash).HasColumnName("PasswordHash");
            builder.Property(x=>x.PasswordSalt).HasColumnName("PasswordSalt");
            builder.Property(x=>x.UserName).HasColumnName("UserName");
        }
    }
}