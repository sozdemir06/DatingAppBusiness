using DatingApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatingApp.DataAccess.Concrete.EntityFramework.Mappings
{
    public class MessagesMap : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages");
            builder.HasKey(x=>x.Id);

            builder.Property(x=>x.IsRead).HasColumnName("IsRead");
            builder.Property(x=>x.MessageSent).HasColumnName("MessageSent");
            builder.Property(x=>x.SenderId).HasColumnName("SenderId");
            builder.Property(x=>x.RecipientId).HasColumnName("RecipientId");
            builder.Property(x=>x.Content).HasColumnName("Content");
            builder.Property(x=>x.DateRead).HasColumnName("DateRead");
            builder.Property(x=>x.SenderDeleted).HasColumnName("SenderDeleted");
            builder.Property(x=>x.RecipientDeleted).HasColumnName("RecipientDeleted");

        }
    }
}