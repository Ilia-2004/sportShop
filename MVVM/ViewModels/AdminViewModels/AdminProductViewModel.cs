using System.Linq;
using System.Windows;
using sportShop.EntityFramework.Models;
using sportShop.MVVM.RelayCommands;
using sportShop.MVVM.ViewModels.ProductViewModels;
using sportShop.MVVM.Views;
using AdminRegistrationView = sportShop.MVVM.Views.AdminViews.AdminRegistrationView;

namespace sportShop.MVVM.ViewModels.AdminViewModels;

/// <summary>
/// Реализация ViewModel для администратора и продукта
/// </summary>
public class AdminProductViewModel : ProductViewModel
{
  #region Свойства комманд

  public RelayCommand AddProductCommand { get; private set; }
  public RelayCommand NavigateAdminRegistrationPage { get; private set; }
  public RelayCommand<Product> DeleteProductCommand { get; private set; }

  #endregion

  /// <summary>
  /// Конструктор по умолчанию
  /// </summary>
  public AdminProductViewModel()
  {
    NavigateAdminRegistrationPage = new RelayCommand(NavigateAdminRegistrationPageCommandExecute);
    AddProductCommand = new RelayCommand(AddProductCommandExecute);
    DeleteProductCommand = new RelayCommand<Product>(DeleteProductCommandExecute);
  }

  #region Реализация команд

  /// <summary>
  /// Команда навигации 
  /// </summary>
  private void NavigateAdminRegistrationPageCommandExecute()
  {
    Context.SaveChanges();

    var mainWindow = Application.Current.MainWindow as MainView;
    mainWindow?.MainFrame.NavigationService.Navigate(new AdminRegistrationView());
  }

  /// <summary>
  /// Команда удаления продукта
  /// </summary>
  /// <param name="product"></param>
  private void DeleteProductCommandExecute(Product? product)
  {
    Context.Products.Remove(product);

    Products.Remove(product);
  }

  /// <summary>
  /// Команда добавления продукта
  /// </summary>
  private void AddProductCommandExecute()
  {
    var product = new Product
    {
      Name = $"Product{Products.Count}", ProductTypeId = Context.ProductTypes.First().Id,
      ProductType = Context.ProductTypes.First(),
      Fabric = Context.Fabrics.First(), FabricId = Context.Fabrics.First().Id, Price = 0, Sale = 0, ProductCount = 1
    };

    var fabric = Context.Fabrics.Find(product.FabricId);

    fabric?.Products.Add(product);
    Context.Products.Add(product);

    Products.Add(product);
  }

  #endregion
}