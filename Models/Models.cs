using System.Collections.Generic;

namespace sportShop.Models
{
    /// <summary>
    /// описание таблиц базы
    /// </summary>
    #region Tabels
    /* таблица пользователей */
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }
    }

    /* таблица продуктов */
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
        public double Price { get; set; }
        public int Fabrik { get; set; }
    }

    /* таблица производителей */
    public class Fabrik
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
    }

    /* таблица скидкок на продукты */
    public class SaleProduct
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
    }

    /* таблица клиентов */
    public class Client
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
    #endregion
}
