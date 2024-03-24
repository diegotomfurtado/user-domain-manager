using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Context
{
	public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var absolutePath = AppDomain.CurrentDomain.BaseDirectory;
            var fatherPath = Directory.GetParent(absolutePath)?.Parent.Parent.Parent.Parent.Parent?.FullName;
            var fullPath = Path.Combine(fatherPath, "Data.Repository/database.db");

            optionsBuilder.UseSqlite($"Data Source={fullPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserDbContext).Assembly);
        }
    }
}

