﻿using Microsoft.EntityFrameworkCore;
using sportShop.Models;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace sportShop;

/// <summary>
/// подключение базы данных
/// </summary>
public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    /* коннект к базы данных */
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
      optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=Boec_UFC1123");

    /// <summary>
    /// добавление таблиц
    /// </summary>
    #region CallTabeles
    public DbSet<Product> Products { get; set; }
    public DbSet<Fabric> Fabrics { get; set; }
    public DbSet<Manager> Managers { get; set; }
    public DbSet<Administrator> Administrators { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<ProductTypes> ProductTypes { get; set; }
    #endregion

    #region Methods
    #endregion
}
