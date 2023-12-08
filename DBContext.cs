using Microsoft.EntityFrameworkCore;
using sportShop.Models;
using System.Collections.Generic;
using System.Linq;

namespace sportShop
{
    internal class DBContext
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
      /// добавление таблиц
      /// </summary>
      #region CallTabeles
      public DbSet<User> Users { get; set; }
      public DbSet<Product> Products { get; set; }
      public DbSet<Fabrik> Fanches { get; set; }
      public DbSet<SaleProduct> SaleProducts { get; set; } 
      public DbSet<Client> clients { get; set; }
      #endregion

      #region Methods 
      public List<Product> GetProducts() => Products.ToList();
      #endregion
    }
  }
}
