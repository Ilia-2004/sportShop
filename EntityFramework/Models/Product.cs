using System.ComponentModel;

namespace sportShop.EntityFramework.Models;

/// <summary>
/// Описание таблицы продукта
/// </summary>
public sealed class Product : INotifyPropertyChanged
{
  public int Id { get; set; }
  public required string Name { get; set; }
  public required int ProductTypeId { get; set; }
  public required ProductTypes ProductType { get; set; }
  public required int ProductCount { get; set; }

  private double _price;

  public required double Price
  {
    get => _price;
    set
    {
      _price = value;
      DiscountedPrice = GetDiscountedPrice();
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Price)));
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DiscountedPrice)));
    }
  }

  private int _sale;

  public required int Sale
  {
    get => _sale;
    set
    {
      _sale = value switch
      {
        < 0 => 0,
        > 100 => 100,
        _ => value
      };

      DiscountedPrice = GetDiscountedPrice();
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Sale)));
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DiscountedPrice)));
    }
  }

  public double DiscountedPrice { get; private set; }

  public double GetDiscountedPrice()
  {
    var discount = (double)_sale / 100;
    var discountedPrice = _price - _price * discount;
    return discountedPrice;
  }

  public required int FabricId { get; set; }
  public required Fabric Fabric { get; set; }
  public event PropertyChangedEventHandler? PropertyChanged;
}