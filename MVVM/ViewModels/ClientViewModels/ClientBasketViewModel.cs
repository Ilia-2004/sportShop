using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using sportShop.EntityFramework;
using sportShop.EntityFramework.Models;
using sportShop.Views.ClientPages;
using ClientProductView = sportShop.MVVM.Views.ClientViews.ClientProductView;

namespace sportShop.MVVM.ViewModels.ClientViewModels;

public sealed class ClientBasketViewModel : BaseViewModel
{
    private readonly Context _context;

    private ObservableCollection<ProductGroup> _productGroups;

    public ObservableCollection<ProductGroup> ProductGroups
    {
        get => _productGroups;
        private set
        {
            _productGroups = value;
            SetPrice();
            OnPropertyChanged();
        }
    }

    private readonly Client _client;

    private string _totalPriceWithOutSale;

    public string TotalPriceWithOutSale
    {
        get => _totalPriceWithOutSale;
        private set
        {
            _totalPriceWithOutSale = value;
            OnPropertyChanged();
        }
    }

    private string _totalSale;

    public string TotalSale
    {
        get => _totalSale;
        private set
        {
            _totalSale = value;
            OnPropertyChanged();
        }
    }

    private string _totalPriceWithSale;

    public string TotalPriceWithSale
    {
        get => _totalPriceWithSale;
        private set
        {
            _totalPriceWithSale = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand NavigateClientProductPage { get; private set; }
    public RelayCommand DeleteProductsCommand { get; private set; }
    public RelayCommand BuyProductsCommand { get; private set; }

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

    private void ProductsGroupChanged(object? sender, PropertyChangedEventArgs e)
    {
        SetPrice();
    }

    private void BuyProductsCommandExecute()
    {
        var selectedProducts = ProductGroups.Where(productGroup => productGroup.IsSelected).ToList();
        if (selectedProducts.Count < 0)   
            return; 

        foreach (var selectedProduct in selectedProducts)
        {
            _context.Products.First(product => product.Id == selectedProduct.Product.Id).ProductCount -= selectedProduct.Count;
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

    private void NavigateClientProductPageExecute()
    {
        var mainWindow = Application.Current.MainWindow as MainWindow;
        mainWindow?.MainFrame.NavigationService.Navigate(
            new ClientProductView(_context.Clients.First(client => client.Id == _client.Id)));

        _context.SaveChanges();
    }

    private void SetPrice()
    {
        TotalPriceWithOutSale = _productGroups.Sum(group => group.Price).ToString(CultureInfo.InvariantCulture);
        TotalPriceWithSale = _productGroups.Sum(group => group.DiscountedPrice).ToString(CultureInfo.InvariantCulture);
        TotalSale =
            (_productGroups.Sum(group => group.Price) / _productGroups.Sum(group => group.DiscountedPrice)).ToString(CultureInfo
                .InvariantCulture);
    }
}

public class ProductGroup
{
    public bool IsSelected { get; set; }
    public Product Product { get; }

    public ProductGroup(Product product, int count)
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