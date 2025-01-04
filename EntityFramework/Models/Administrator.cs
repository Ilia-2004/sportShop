namespace sportShop.EntityFramework.Models;

/// <summary>
/// Описание таблицы администратора
/// </summary>
public class Administrator
{
  public int Id { get; set; }
  public required string Login { get; set; }
  public required string Password { get; set; }
}