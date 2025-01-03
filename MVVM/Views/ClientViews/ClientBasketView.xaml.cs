using sportShop.EntityFramework.Models;
using sportShop.MVVM.ViewModels.ClientViewModels;

namespace sportShop.MVVM.Views.ClientViews;

public partial class ClientBasketView
{
    public ClientBasketView(Client client)
    {
        DataContext = new ClientBasketViewModel(client);

        InitializeComponent();
    }
}