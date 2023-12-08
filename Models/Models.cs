using System.Collections.Generic;

namespace sportShop.Models
{
    /// <summary>
    /// описание таблиц базы
    /// </summary>
    #region Tabels
    /* таблица пользователей */
    public class Client
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
        public ICollection<Product>? Products { get; set; }
    }

    public class Manager
    {
        public int Id { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
    }

    public class Administraitor
    {
        public int Id { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
    }

    /* таблица продуктов */
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required ProductTypes Type { get; set; }
        public double Price { get; set; }
        public int? FabrikId { get; set; }
        public Fabrik? Fabrik { get; set; }
    }

    /* таблица производителей */
    public class Fabrik
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }   
        public ICollection<Product> ? Products { get; set; }
    }

    public class ProductTypes
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
    #endregion
}
