using System.ComponentModel;
using System.Linq;

namespace sportShop.EntityFramework.Models;

public class ProductGroup
{
  public bool IsSelected { get; set; }
  public Product? Product { get; }

  public ProductGroup(Product? product, int count)
  {
    Product = product;
    Count = count;
  }

  private int _count;

  public int Count
  {
    get => _count;
    set
    {
      var dbContext = new Context();
      var maxCountOfProducts = dbContext.Products.First(prod => prod.Id == Product.Id).ProductCount;
      _count = value > maxCountOfProducts? maxCountOfProducts : value;

      Price = _count * Product.Price;
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
    }
  }

  private double _price;

  public double Price
  {
    get => _price;
    set
    {
      _price = value;
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Price)));
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DiscountedPrice)));
    }
  }

  public double DiscountedPrice
  {
    get
    {
      var discount = (double) Product.Sale / 100;
      var discountedPrice = _price - _price * discount;
      return discountedPrice;
    }
  }

  public event PropertyChangedEventHandler? PropertyChanged;
}