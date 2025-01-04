using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using sportShop.EntityFramework.Models;
using sportShop.MVVM.RelayCommands;
using sportShop.MVVM.ViewModels.ProductViewModels;
using sportShop.MVVM.Views;
using ClientBasketView = sportShop.MVVM.Views.ClientViews.ClientBasketView;

namespace sportShop.MVVM.ViewModels.ClientViewModels;

/// <summary>
/// Реализация ViewModel для клиента и продукта
/// </summary>
public class ClientProductViewModel : ProductViewModel
{
  /* Свойство передаваемого клиента */
  private readonly Client _client;

  #region Свойства команд

  public RelayCommand<Product> AddToBucketCommand { get; private set; }
  public RelayCommand NavigateClientBasket { get; private set; }

  #endregion

  /// <summary>
  /// Конструктор по умолчанию
  /// </summary>
  /// <param name="client"></param>
  public ClientProductViewModel(Client client)
  {
    _client = client;

    NavigateClientBasket = new RelayCommand(NavigateClientBasketExecute);
    AddToBucketCommand = new RelayCommand<Product>(AddToBucketCommandExecute);

    Products = new ObservableCollection<Product?>(Context.Products.Where(product => product.ProductCount > 0)
      .Include(c => c.Fabric).Include(c => c.ProductType));
  }

  #region Реализация команд

  /// <summary>
  /// Команда навигации
  /// </summary>
  private void NavigateClientBasketExecute()
  {
    Context.SaveChanges();

    var mainWindow = Application.Current.MainWindow as MainView;
    var clientBasketView = new ClientBasketView(Context.Clients.First(c => c.Id == _client.Id));

    mainWindow?.MainFrame.NavigationService.Navigate(clientBasketView);
  }

  /// <summary>
  /// Команда добавления в корзину
  /// </summary>
  /// <param name="product"></param>
  private void AddToBucketCommandExecute(Product? product) =>
    Context.Clients.First(c => c.Id == _client.Id).Products.Add(product);

  #endregion
}