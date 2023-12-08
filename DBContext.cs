using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace sportShop
{
  internal abstract class DBContext
  {
    /// <summary>
    /// подключение базы данных
    /// </summary>
    public class ApplicationContext : DbContext
    {
      /* коннект к базы данных */ 
      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=12345Qq");

      /// <summary>
      /// вызов таблиц
      /// </summary>
      #region CallTabeles
      public DbSet<User> Users { get; set; } = null!;
      public DbSet<Product> Products {  get; set; } = null!;
      public DbSet<Fabrik> Fanches { get; set; } = null!;
      public DbSet<SaleProduct> SaleProducts { get; set; } = null!;
      public DbSet<Client> clients { get; set; } = null!;
      #endregion
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
      public required string Name { get; set; }
      public int Age { get; set; }
    }

    /* таблица продуктов */
    public class Product 
    {
      [Key]
      public int Id { get; set; }
      public required string Name { get; set; }
      public required string Type { get; set; }
      public double Price { get; set; }
      public int   Fabrik { get; set; }
    }
   
    /* таблица производителей */
    public class Fabrik
    {
      [Key]
      public int Id { get; set; }
      public required string Name { get; set; }
      public required string Type { get; set; }
      public double Price { get; set; }
    }

    /* таблица скидкок на продукты */
    public class SaleProduct
    {
      [Key]
      public int Id { get; set; }
      public required string Name { get; set; }
      public required string Type { get; set; }
    }

    /* таблица клиентов */
    public class Client
    {
      [Key]
      public int Id { get; set; }
      public required string Name { get; set; }
      public required string Type { get; set; }
    }
    #endregion
  }
}
