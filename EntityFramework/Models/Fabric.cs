using System.Collections.Generic;

namespace sportShop.EntityFramework.Models;

public class Fabric
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}