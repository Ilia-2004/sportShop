using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using sportShop.Models;
using sportShop.Pages.AdminPages;
using sportShop.Pages.ClientPages;
using sportShop.Pages.GeneralPages;

namespace sportShop.ViewModels;

sealed public class ProductPageViewModel : INotifyPropertyChanged
{
    private readonly DbContext _dbContext;

    public RelayCommand AddProductCommand { get; private set; }
    public RelayCommand NavigateClientBasket { get; private set; }
    public RelayCommand NavigateAuthorisationPage { get; private set; }
    public RelayCommand NavigateAdminRegistrationPage { get; private set; }
    public RelayCommand<Product> DeleteProductCommand { get; private set; }
    public RelayCommand<Product> AddToBucketCommand { get; private set; }


    private ObservableCollection<Product> _products;

    public ObservableCollection<Product> Products
    {
        get => _products;
        set
        {
            _products = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Products)));
            _dbContext.SaveChanges();
        }
    }

    private readonly Client? _client;

    public Client? Client
    {
        get => _client;
        init
        {
            _client = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Client)));
        }
    }

    public ProductPageViewModel()
    {
        _dbContext = new DbContext();
        _products = new ObservableCollection<Product>(_dbContext.Products.Include(c => c.Fabric).Include(c => c.ProductType));

        NavigateClientBasket = new RelayCommand(NavigateClientBasketExecute);
        NavigateAdminRegistrationPage = new RelayCommand(NavigateAdminRegistrationPageExecute);
        NavigateAuthorisationPage = new RelayCommand(NavigateAuthorisationPageExecute);
        AddProductCommand = new RelayCommand(AddProductCommandExecute);
        DeleteProductCommand = new RelayCommand<Product>(DeleteProductCommandExecute);
        AddToBucketCommand = new RelayCommand<Product>(AddToBucketCommandExecute);
    }

    private void NavigateAdminRegistrationPageExecute()
    {
        _dbContext.SaveChanges();

        var mainWindow = Application.Current.MainWindow as MainWindow;
        mainWindow?.MainFrame.NavigationService.Navigate(new AdminRegistrationView());
    }
    private void NavigateAuthorisationPageExecute()
    {
        _dbContext.SaveChanges();

        var mainWindow = Application.Current.MainWindow as MainWindow;
        mainWindow?.MainFrame.NavigationService.Navigate(new AuthorizationView());
    }

    private void NavigateClientBasketExecute()
    {
        _dbContext.SaveChanges();

        var mainWindow = Application.Current.MainWindow as MainWindow;
        var clientBasketView = new ClientBasketView
        {
            DataContext = new ClientBasketViewModel(_dbContext.Clients.First(c => Client != null && c.Id == Client.Id))
        };

        mainWindow?.MainFrame.NavigationService.Navigate(clientBasketView);
    }

    private void AddToBucketCommandExecute(Product product)
    {
        if (_dbContext.Clients.Include(client => client.Products).First(c => Client != null && c.Id == Client.Id).Products
            .Contains(product))
        {
            MessageBox.Show("Товар уже в корзине, дорогуша!");
            return;
        }

        _dbContext.Clients.First(c => Client != null && c.Id == Client.Id).Products.Add(product);
        _dbContext.SaveChanges();
    }

    private void DeleteProductCommandExecute(Product product)
    {
        _dbContext.Products.Remove(product);
        _dbContext.SaveChanges();

        _products = new ObservableCollection<Product>(_dbContext.Products.Include(c => c.Fabric).Include(c => c.ProductType));

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Products)));
    }

    private void AddProductCommandExecute()
    {
        var product = new Product
        {
            Name = $"Product{_dbContext.Products.Count()}", ProductTypeId = _dbContext.ProductTypes.First().Id,
            ProductType = _dbContext.ProductTypes.First(),
            Fabric = _dbContext.Fabrics.First(), FabricId = _dbContext.Fabrics.First().Id, Price = 0, Sale = 0
        };

        var fabric = _dbContext.Fabrics.Find(product.FabricId);

        fabric?.Products.Add(product);
        _dbContext.Products.Add(product);

        _dbContext.SaveChanges();
        _products = new ObservableCollection<Product>(_dbContext.Products.Include(c => c.Fabric).Include(c => c.ProductType));

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Products)));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}