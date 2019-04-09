using DatingApp.DataAccess.Concrete.EntityFramework.Mappings;
using DatingApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.DataAccess.Concrete.EntityFramework
{
    public class DataContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=DatingApp;Username=postgres;Password=466357",b=>b.MigrationsAssembly("DatingApp.WebAPI"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ValueMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new LikeMap());
            modelBuilder.ApplyConfiguration(new PhotoMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new MessagesMap());
            modelBuilder.ApplyConfiguration(new UserRoleMap());

            modelBuilder.Entity<Like>()
                        .HasKey(k=>new {k.LikerId,k.LikeeId});
            modelBuilder.Entity<Like>()
                .HasOne(u => u.Likee)
                .WithMany(u => u.Likers)
                .HasForeignKey(u => u.LikeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Like>()
                .HasOne(u => u.Liker)
                .WithMany(u => u.Likees)
                .HasForeignKey(u => u.LikerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                        .HasOne(u=>u.Sender)
                        .WithMany(m=>m.MessageSent)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                        .HasOne(u=>u.Recipient)
                        .WithMany(m=>m.MessagesRecevied)
                        .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<UserRole>(UserRole=>{
                UserRole.HasKey(ur=>new {ur.UserId,ur.RoleId});
                UserRole.HasOne(ur=>ur.Role)
                        .WithMany(r=>r.UserRoles)
                        .HasForeignKey(f=>f.RoleId)
                        .IsRequired();
                UserRole.HasOne(ur=>ur.User)
                        .WithMany(r=>r.UserRoles)
                        .HasForeignKey(f=>f.UserId)
                        .IsRequired();        

            });
        }

        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Like> Likers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}