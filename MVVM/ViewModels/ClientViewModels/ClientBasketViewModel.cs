using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using sportShop.EntityFramework;
using sportShop.EntityFramework.Models;
using sportShop.MVVM.RelayCommands;
using sportShop.MVVM.Views;
using ClientProductView = sportShop.MVVM.Views.ClientViews.ClientProductView;

namespace sportShop.MVVM.ViewModels.ClientViewModels;

/// <summary>
/// Реализация ViewModel для клиента и корзины
/// </summary>
public sealed class ClientBasketViewModel : BaseViewModel
{
  #region Свойства

  private readonly Context _context;
  private readonly Client _client;

  private ObservableCollection<ProductGroup> _productGroups;

  public ObservableCollection<ProductGroup> ProductGroups
  {
    get => _productGroups;
    private set
    {
      _productGroups = value;
      SetPrice();
      OnPropertyChanged(nameof(ProductGroups));
    }
  }

  private string _totalPriceWithOutSale;

  public string TotalPriceWithOutSale
  {
    get => _totalPriceWithOutSale;
    private set
    {
      _totalPriceWithOutSale = value;
      OnPropertyChanged(nameof(TotalPriceWithOutSale));
    }
  }

  private string _totalSale;

  public string TotalSale
  {
    get => _totalSale;
    private set
    {
      _totalSale = value;
      OnPropertyChanged(nameof(TotalSale));
    }
  }

  private string _totalPriceWithSale;

  public string TotalPriceWithSale
  {
    get => _totalPriceWithSale;
    private set
    {
      _totalPriceWithSale = value;
      OnPropertyChanged(nameof(TotalPriceWithSale));
    }
  }

  #endregion

  #region Свойства команд

  public RelayCommand NavigateClientProductPage { get; private set; }
  public RelayCommand DeleteProductsCommand { get; private set; }
  public RelayCommand BuyProductsCommand { get; private set; }

  #endregion

  /// <summary>
  /// Конструктор по умолчанию
  /// </summary>
  /// <param name="client"></param>
  public ClientBasketViewModel(Client client)
  {
    _context = new Context();

    _totalSale = string.Empty;
    _totalPriceWithSale = string.Empty;
    _totalPriceWithOutSale = string.Empty;

    _client = client;

    _productGroups = new ObservableCollection<ProductGroup>(client.Products
      .GroupBy(p => p.Id)
      .Select(g => new ProductGroup(
        _context.Products.Include(c => c.Fabric).Include(c => c.ProductType).First(pr => pr.Id == g.Key),
        g.Count())).ToList());

    foreach (var productGroup in _productGroups)
      productGroup.PropertyChanged += ProductsGroupChanged;
    SetPrice();

    NavigateClientProductPage = new RelayCommand(NavigateClientProductPageExecute);
    DeleteProductsCommand = new RelayCommand(DeleteProductsCommandExecute);
    BuyProductsCommand = new RelayCommand(BuyProductsCommandExecute);
  }

  #region Методы

  /// <summary>
  /// Метод изменения группы продуктов
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ProductsGroupChanged(object? sender, PropertyChangedEventArgs e) => SetPrice();

  /// <summary>
  /// Метод установления цены
  /// </summary>
  private void SetPrice()
  {
    TotalPriceWithOutSale = _productGroups.Sum(group => group.Price).ToString(CultureInfo.InvariantCulture);
    TotalPriceWithSale = _productGroups.Sum(group => group.DiscountedPrice).ToString(CultureInfo.InvariantCulture);
    TotalSale =
      (_productGroups.Sum(group => group.Price) / _productGroups.Sum(group => group.DiscountedPrice)).ToString(
        CultureInfo
          .InvariantCulture);
  }

  #endregion

  #region Реализация команд

  /// <summary>
  /// Команда покупки
  /// </summary>
  private void BuyProductsCommandExecute()
  {
    var selectedProducts = ProductGroups.Where(productGroup => productGroup.IsSelected).ToList();
    if (selectedProducts.Count < 0)
      return;

    foreach (var selectedProduct in selectedProducts)
    {
      _context.Products.First(product => product.Id == selectedProduct.Product.Id).ProductCount -=
        selectedProduct.Count;
      _context.Clients.First(client => client.Id == _client.Id).Products
        .Remove(_context.Products.First(prod => prod.Id == selectedProduct.Product.Id));
    }

    _context.SaveChanges();

    ProductGroups = new ObservableCollection<ProductGroup>(_context.Clients.Include(client => client.Products)
      .First(c => c.Id == _client.Id).Products
      .GroupBy(p => p.Id)
      .Select(g => new ProductGroup(
        _context.Products.Include(c => c.Fabric).Include(c => c.ProductType).FirstOrDefault(pr => pr.Id == g.Key),
        g.Count())).ToList());

    SetPrice();
    MessageBox.Show("Product bought!");
  }

  /// <summary>
  /// Команда удаления продукта
  /// </summary>
  private void DeleteProductsCommandExecute()
  {
    var selectedProducts = ProductGroups.Where(productGroup => productGroup.IsSelected)
      .Select(productGroup => productGroup.Product).ToList();

    foreach (var selectedProduct in selectedProducts)
      _context.Clients.First(c => c.Id == _client.Id).Products
        .Remove(_context.Products.First(prod => prod.Id == selectedProduct.Id));
    _context.SaveChanges();

    ProductGroups = new ObservableCollection<ProductGroup>(_context.Clients.Include(client => client.Products)
      .First(c => c.Id == _client.Id).Products
      .GroupBy(p => p.Id)
      .Select(g => new ProductGroup(
        _context.Products.Include(c => c.Fabric).Include(c => c.ProductType).FirstOrDefault(pr => pr.Id == g.Key),
        g.Count())).ToList());

    SetPrice();
  }

  /// <summary>
  /// Команда навигации
  /// </summary>
  private void NavigateClientProductPageExecute()
  {
    var mainWindow = Application.Current.MainWindow as MainView;
    mainWindow?.MainFrame.NavigationService.Navigate(
      new ClientProductView(_context.Clients.First(client => client.Id == _client.Id)));

    _context.SaveChanges();
  }

  #endregion
}