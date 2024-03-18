using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Context
{
	public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("conf/appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("DataBase");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}

