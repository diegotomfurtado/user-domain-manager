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

        protected override void OnConfiguring(DbContextOptionsBuilder contextOptionsBuilder)
        {
            contextOptionsBuilder.UseSqlServer("Server=tcp:diegotomfurtado-ecommerce.database.windows.net,1433;Initial Catalog=ecommerce_teste;Encrypt=True;TrustServerCertificate=False;user=diegotomfurtado@diegotomfurtado-ecommerce;password=7&3JGXsK*9;Connection Timeout=30;");
            base.OnConfiguring(contextOptionsBuilder);
        }
    }
}

