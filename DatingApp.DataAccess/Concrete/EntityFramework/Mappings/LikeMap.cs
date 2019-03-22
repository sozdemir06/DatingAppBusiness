using DatingApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.DataAccess.Concrete.EntityFramework.Mappings
{
    public class LikeMap : IEntityTypeConfiguration<Like>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Like> builder)
        {
            builder.ToTable("Likers");
           

            builder.Property(x=>x.LikeeId).HasColumnName("LikeeId");
            builder.Property(x=>x.LikerId).HasColumnName("LikerId");
        }
    }
}