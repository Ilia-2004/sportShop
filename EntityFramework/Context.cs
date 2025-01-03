using Microsoft.EntityFrameworkCore;
using sportShop.EntityFramework.Models;

namespace sportShop.EntityFramework;

/// <summary>
/// Подключение базы данных
/// </summary>
public class Context : DbContext
{    
    #region Объекты базы данных
    public DbSet<Product> Products { get; set; }
    public DbSet<Fabric> Fabrics { get; set; }
    public DbSet<Manager> Managers { get; set; }
    public DbSet<Administrator> Administrators { get; set; }
    public DbSet<Client> Clients { get; set; } 
    public DbSet<ProductTypes> ProductTypes { get; set; }
    #endregion

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseLazyLoadingProxies()
            .UseNpgsql(@"Server=DESKTOP-34SGMAN\LOCALDB;Database=RestaurantProgramDB;Trusted_Connection=True;TrustServerCertificate=True;");
}
