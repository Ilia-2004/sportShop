using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using sportShop.EntityFramework;
using sportShop.EntityFramework.Models;
using sportShop.Views.GeneralPages;
using AuthorizationView = sportShop.MVVM.Views.GeneralViews.AuthorizationView;

namespace sportShop.MVVM.ViewModels.ProductViewModels;

public class ProductViewModel : BaseViewModel
{
    protected readonly Context Context;

    public RelayCommand NavigateAuthorisationPage { get; private set; }

    private readonly ObservableCollection<Product> _products;

    public ObservableCollection<Product> Products
    {
        get => _products;
        protected init
        {
            _products = value;
            OnPropertyChanged();
        }
    }

    public ProductViewModel()
    {
        Context = new Context();
        _products = new ObservableCollection<Product>(Context.Products.Include(c => c.Fabric).Include(c => c.ProductType));

        NavigateAuthorisationPage = new RelayCommand(NavigateAuthorisationPageExecute);
    }

    private void NavigateAuthorisationPageExecute()
    {
        Context.SaveChanges();

        var mainWindow = Application.Current.MainWindow as MainWindow;
        mainWindow?.MainFrame.NavigationService.Navigate(new AuthorizationView());
    }
}