using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using sportShop.Models;
using sportShop.Pages.ClientPages;

namespace sportShop.ViewModels;

sealed public class ClientBasketViewModel : INotifyPropertyChanged
{
    private readonly DbContext _dbContext;

    private ObservableCollection<Product>? _products;

    public ObservableCollection<Product>? Products
    {
        get => _products;
        init
        {
            _products = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Products)));
        }
    }

    private readonly Client _client;

    public Client Client
    {
        get => _client;
        init
        {
            _client = value;
            Products = new ObservableCollection<Product>(_client.Products);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Client)));
        }
    }

    private string _totalPriceWithOutSale;

    public string TotalPriceWithOutSale
    {
        get => _totalPriceWithOutSale;
        set
        {
            _totalPriceWithOutSale = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalPriceWithOutSale)));
        }
    }

    private string _totalSale;

    public string TotalSale
    {
        get => _totalSale;
        set
        {
            _totalSale = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalSale)));
        }
    }

    private string _totalPriceWithSale;

    public string TotalPriceWithSale
    {
        get => _totalPriceWithSale;
        set
        {
            _totalPriceWithSale = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalPriceWithSale)));
        }
    }

    public RelayCommand<Product> DeleteProductCommand { get; private set; }
    public RelayCommand NavigateClientProductPage { get; private set; }

    public ClientBasketViewModel(Client client)
    {
        _products = new ObservableCollection<Product>();
        _dbContext = new DbContext();
        _client = client;
        _products = new ObservableCollection<Product>(_client.Products);

        SetPrice();

        DeleteProductCommand = new RelayCommand<Product>(DeleteProductCommandExecute);
        NavigateClientProductPage = new RelayCommand(NavigateClientProductPageExecute);
    }

    private void NavigateClientProductPageExecute()
    {
        var mainWindow = Application.Current.MainWindow as MainWindow;
        mainWindow?.MainFrame.NavigationService.Navigate(
            new ClientProductView(_dbContext.Clients.First(client => client.Id == Client.Id)));

        _dbContext.SaveChanges();
    }

    private void DeleteProductCommandExecute(Product product)
    {
        _client.Products.Remove(product);
        _products = new ObservableCollection<Product>(_client.Products);
        SetPrice();

        _dbContext.Clients.First(c => c.Id == _client.Id).Products.Remove(_dbContext.Products.First(prod => prod.Id == product.Id));

        _dbContext.SaveChanges();
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Products)));
    }

    private void SetPrice()
    {
        TotalPriceWithSale =
            _products is not null? _products.Sum(c => c.DiscountedPrice).ToString(CultureInfo.InvariantCulture) : "0";
        TotalSale = _products is not null
            ? _products.Sum(c => c.Price - c.DiscountedPrice).ToString(CultureInfo.InvariantCulture)
            : "0";
        TotalPriceWithOutSale = _products is not null? _products.Sum(c => c.Price).ToString(CultureInfo.InvariantCulture) : "0";
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}