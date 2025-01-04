using Microsoft.EntityFrameworkCore;
using sportShop.EntityFramework.Models;

namespace sportShop.EntityFramework;

/// <summary>
/// Реализация контекста модели
/// </summary>
public sealed class Context : DbContext
{
  #region Объекты базы данных

  public DbSet<Product?> Products { get; set; }
  public DbSet<Fabric> Fabrics { get; set; }
  public DbSet<Manager> Managers { get; set; }
  public DbSet<Administrator> Administrators { get; set; }
  public DbSet<Client> Clients { get; set; }
  public DbSet<ProductTypes> ProductTypes { get; set; }

  #endregion

  /// <summary>
  /// Конструктор по умолчанию
  /// </summary>
  public Context() => Database.EnsureCreated();

  /// <summary>
  /// Строка подключения к базе данных
  /// </summary>
  /// <param name="optionsBuilder"></param>
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
    optionsBuilder.UseLazyLoadingProxies()
      .UseNpgsql(
        @"Server=DESKTOP-34SGMAN\LOCALDB;Database=SportShopDb;Trusted_Connection=True;TrustServerCertificate=True;");

  /// <summary>
  /// Метод для наполнения базы данных начальными данными
  /// </summary>
  /// <param name="modelBuilder"></param>
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    #region Инициализация объектов для таблиц

    var firstProductTypes = new ProductTypes { Id = 1, Name = "Tapok" };
    var firstAdministrator = new Administrator { Id = 1, Login = "Admin", Password = "Admin" };
    var firstFabric = new Fabric { Id = 1, Name = "Fabrik" };

    #endregion

    #region Добавление данных в базу данных

    modelBuilder.Entity<ProductTypes>().HasData(firstProductTypes);
    modelBuilder.Entity<Administrator>().HasData(firstAdministrator);
    modelBuilder.Entity<Fabric>().HasData(firstFabric);

    #endregion
  }
}