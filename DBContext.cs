using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace sportShop
{
  internal class DBContext
  {
    /* подключение базы данных */
    public class ApplicationContext : DbContext
    {
      public DbSet<User> Users { get; set; } = null!;
 
      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=12345Qq");
    }

    #region Tabels
    public class User
    {
      [Key]
      public int Id { get; set; }
      public string? Name { get; set; }
      public int Age { get; set; }
    }


    #endregion
  }
}
