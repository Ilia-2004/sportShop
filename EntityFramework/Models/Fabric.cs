using System.Collections.Generic;

namespace sportShop.EntityFramework.Models;

/// <summary>
/// Описание таблицы материала
/// </summary>
public class Fabric
{
  public int Id { get; set; }
  public required string Name { get; set; }
  public virtual ICollection<Product?> Products { get; set; } = new List<Product?>();
}