using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using sportShop.EntityFramework;
using sportShop.EntityFramework.Models;
using sportShop.MVVM.RelayCommands;
using sportShop.MVVM.Views;
using AuthorizationView = sportShop.MVVM.Views.GeneralViews.AuthorizationView;

namespace sportShop.MVVM.ViewModels.ProductViewModels;

/// <summary>
/// Реализация ViewModel продукта
/// </summary>
public class ProductViewModel : BaseViewModel
{
  #region Свойства

  protected readonly Context Context;

  private readonly ObservableCollection<Product?> _products;

  public ObservableCollection<Product?> Products
  {
    get => _products;
    protected init
    {
      _products = value;
      OnPropertyChanged();
    }
  }

  #endregion

  /* Свойство команды */
  public RelayCommand NavigateAuthorisationPage { get; private set; }

  /// <summary>
  /// Конструктор по умолчанию
  /// </summary>
  public ProductViewModel()
  {
    Context = new Context();
    _products = new ObservableCollection<Product?>(Context.Products.Include(c => c.Fabric).Include(c => c.ProductType));

    NavigateAuthorisationPage = new RelayCommand(NavigateAuthorisationPageExecute);
  }

  /// <summary>
  /// Команда навигации 
  /// </summary>
  private void NavigateAuthorisationPageExecute()
  {
    Context.SaveChanges();

    var mainWindow = Application.Current.MainWindow as MainView;
    mainWindow?.MainFrame.NavigationService.Navigate(new AuthorizationView());
  }
}