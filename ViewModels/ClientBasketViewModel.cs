using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using sportShop.Models;
using sportShop.Pages.ClientPages;

namespace sportShop.ViewModels;

sealed public class ClientBasketViewModel : BaseViewModel
{
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
                var dbContext = new DbContext();
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

    private readonly DbContext _dbContext;

    private ObservableCollection<ProductGroup> _productGroups;

    public ObservableCollection<ProductGroup> ProductGroups
    {
        get => _productGroups;
        set
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
        _dbContext = new DbContext();

        _totalSale = string.Empty;
        _totalPriceWithSale = string.Empty;
        _totalPriceWithOutSale = string.Empty;

        _client = client;

        _productGroups = new ObservableCollection<ProductGroup>(client.Products
            .GroupBy(p => p.Id)
            .Select(g => new ProductGroup(
                _dbContext.Products.Include(c => c.Fabric).Include(c => c.ProductType).First(pr => pr.Id == g.Key),
                g.Count())).ToList());

        SetPrice();

        NavigateClientProductPage = new RelayCommand(NavigateClientProductPageExecute);
        DeleteProductsCommand = new RelayCommand(DeleteProductsCommandExecute);
        BuyProductsCommand = new RelayCommand(BuyProductsCommandExecute);
    }

    private void BuyProductsCommandExecute()
    {
      MessageBox.Show("������� �������");
    }

    private void DeleteProductsCommandExecute()
    {
        // ProductGroups.Where(prdgr => prdgr.IsSelected).ToList().ForEach(prdgr => ProductGroups.Remove(prdgr));

        var productsIdToRemove = ProductGroups.Where(prdgr => prdgr.IsSelected).Select(prdtg => prdtg.Product.Id).ToList();
        var productsToRemove = _dbContext.Products.Where(product => productsIdToRemove.Contains(product.Id)).ToList();
        _dbContext.Products.RemoveRange(productsToRemove);
        _dbContext.SaveChanges();

        ProductGroups = new ObservableCollection<ProductGroup>(_client.Products
            .GroupBy(p => p.Id)
            .Select(g => new ProductGroup(
                _dbContext.Products.Include(c => c.Fabric).Include(c => c.ProductType).First(pr => pr.Id == g.Key),
                g.Count())).ToList());
    }

    private void NavigateClientProductPageExecute()
    {
        var mainWindow = Application.Current.MainWindow as MainWindow;
        mainWindow?.MainFrame.NavigationService.Navigate(
            new ClientProductView(_dbContext.Clients.First(client => client.Id == _client.Id)));

        _dbContext.SaveChanges();
    }

    public void SetPrice()
    {
        TotalPriceWithOutSale = _productGroups.Sum(group => group.Price).ToString(CultureInfo.InvariantCulture);
        TotalPriceWithSale = _productGroups.Sum(group => group.DiscountedPrice).ToString(CultureInfo.InvariantCulture);
        TotalSale =
            (_productGroups.Sum(group => group.Price) / _productGroups.Sum(group => group.DiscountedPrice)).ToString(CultureInfo
                .InvariantCulture);
    }
}