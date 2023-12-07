using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace sportShop
{
  internal class DBContext
  {
    /// <summary>
    /// подключение базы данных
    /// </summary>
    public class ApplicationContext : DbContext
    {
      public DbSet<User> Users { get; set; } = null!;
      public DbSet<Product> Products {  get; set; } = null!; 
 
      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=12345Qq");
    }

    /// <summary>
    /// описание таблиц базы
    /// </summary>
    #region Tabels
    /* таблица пользователей */
    public class User
    {
      [Key]
      public int Id { get; set; }
      public string Name { get; set; }
      public int Age { get; set; }
    }

    /* таблица продуктов */
    public class Product 
    {
      [Key]
      public int Id { get; set; }
      public string Name { get; set; }
      public string Type { get; set; }
      public double Price { get; set; }
    }
    #endregion
  }
}
