using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using sportShop.EntityFramework.Models;
using sportShop.MVVM.ViewModels.ProductViewModels;
using sportShop.Views.ClientPages;
using ClientBasketView = sportShop.MVVM.Views.ClientViews.ClientBasketView;

namespace sportShop.MVVM.ViewModels.ClientViewModels;

public class ClientProductViewModel : ProductViewModel
{
    public RelayCommand<Product> AddToBucketCommand { get; private set; }
    public RelayCommand NavigateClientBasket { get; private set; }

    private readonly Client _client;

    public ClientProductViewModel(Client client)
    {
        _client = client;

        NavigateClientBasket = new RelayCommand(NavigateClientBasketExecute);
        AddToBucketCommand = new RelayCommand<Product>(AddToBucketCommandExecute);

        Products = new ObservableCollection<Product>(Context.Products.Where(product => product.ProductCount > 0)
            .Include(c => c.Fabric).Include(c => c.ProductType));
    }

    private void NavigateClientBasketExecute()
    {
        Context.SaveChanges();

        var mainWindow = Application.Current.MainWindow as MainWindow;
        var clientBasketView = new ClientBasketView(Context.Clients.First(c => c.Id == _client.Id));

        mainWindow?.MainFrame.NavigationService.Navigate(clientBasketView);
    }

    private void AddToBucketCommandExecute(Product product)
    {
        Context.Clients.First(c => c.Id == _client.Id).Products.Add(product);
    }
}