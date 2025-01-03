namespace sportShop.EntityFramework.Models;

public class Administrator
{
    public int Id { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
}