namespace sportShop.EntityFramework.Models;

/// <summary>
/// Описание таблицы типа продукта
/// </summary>
public class ProductTypes
{
  public int Id { get; set; }
  public required string Name { get; set; }
}