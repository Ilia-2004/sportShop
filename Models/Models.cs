using System.Collections.Generic;

namespace sportShop.Models;

    #region Tabels

public class Client
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required int Age { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}

public class Manager
{
    public int Id { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
}

public class Administrator
{
    public int Id { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
}

public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required int ProductTypeId { get; set; }
    public required ProductTypes ProductType { get; set; }
    public required double Price { get; set; }
    public required int FabricId { get; set; }
    public required Fabric Fabric { get; set; }
}

public class Fabric
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}

public class ProductTypes
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

    #endregion