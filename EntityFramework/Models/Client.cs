using System.Collections.Generic;

namespace sportShop.EntityFramework.Models;

/// <summary>
/// Описание таблицы пользователя 
/// </summary>
public class Client
{
  public int Id { get; set; }
  public required string Name { get; set; }
  public required int Age { get; set; }
  public required string Login { get; set; }
  public required string Password { get; set; }
  public virtual ICollection<Product?> Products { get; set; } = new List<Product?>();
}