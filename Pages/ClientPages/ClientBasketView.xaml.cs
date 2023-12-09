using sportShop.Models;
using sportShop.ViewModels;

namespace sportShop.Pages.ClientPages;

public partial class ClientBasketView
{
    public ClientBasketView(Client client)
    {
        InitializeComponent();

        var clientBasketViewModel = new ClientBasketViewModel{Client = client};
        DataContext = clientBasketViewModel;
    }
}