using Microsoft.EntityFrameworkCore;

namespace sportShop
{
    internal class DBContext
    {
        public class User
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public int Age { get; set; }
        }

        public class ApplicationContext : DbContext
        {
            public DbSet<User> Users { get; set; } = null!;

            public ApplicationContext()
            {
                Database.EnsureCreated();
            }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=пароль_от_postgres");
            }
        }
    }
}
