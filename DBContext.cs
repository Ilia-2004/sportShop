using Microsoft.EntityFrameworkCore;
using sportShop.Models;
using System.Collections.Generic;
using System.Linq;

namespace sportShop
{
    /// <summary>
    /// подключение базы данных
    /// </summary>
    public class DBContext : DbContext
    {
      /* коннект к базы данных */ 
      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=52");

      /// <summary>
      /// добавление таблиц
      /// </summary>
      #region CallTabeles
      public DbSet<Client> Users { get; set; }
      public DbSet<Product> Products { get; set; }
      public DbSet<Fabrik> Fabrics { get; set; }
      public DbSet<Manager> Managers { get; set; }
      public DbSet<Administraitor> Administraitors { get; set; }
      public DbSet<Client> Clients { get; set; }
      public DbSet<ProductTypes> ProductTypes { get; set; }
      #endregion

      #region Methods 
      #endregion
  }
}
