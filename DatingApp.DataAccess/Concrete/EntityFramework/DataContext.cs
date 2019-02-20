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
        }

        public DbSet<Value> Values { get; set; }
    }
}