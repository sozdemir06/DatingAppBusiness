using DatingApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatingApp.DataAccess.Concrete.EntityFramework.Mappings
{
    public class ValueMap : IEntityTypeConfiguration<Value>
    {
        public void Configure(EntityTypeBuilder<Value> builder)
        {
            builder.ToTable("Values");
            builder.HasKey(x=>x.Id);

            builder.Property(x=>x.Id).HasColumnName("Id");
            builder.Property(x=>x.Name).HasColumnName("Name");
        }
    }
}