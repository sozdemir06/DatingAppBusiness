using DatingApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatingApp.DataAccess.Concrete.EntityFramework.Mappings
{
    public class PhotoMap : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.ToTable("Photos");
            builder.HasKey(x=>x.Id);

            builder.Property(x=>x.Url).HasColumnName("Url");
            builder.Property(x=>x.Description).HasColumnName("Description");
            builder.Property(x=>x.DateAdded).HasColumnName("DateAdded");
            builder.Property(x=>x.IsMain).HasColumnName("IsMain");
            builder.Property(x=>x.UserId).HasColumnName("UserId");
            builder.Property(x=>x.PublicId).HasColumnName("PublicId");

        }
    }
}