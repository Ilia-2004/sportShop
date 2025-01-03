namespace sportShop.EntityFramework.Models;

public class Manager
{
    public int Id { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
}