using sportShop.EntityFramework.Models;
using sportShop.MVVM.ViewModels.ClientViewModels;

namespace sportShop.MVVM.Views.ClientViews;

public partial class ClientProductView
{
    public ClientProductView(Client client)
    {
        InitializeComponent();

        DataContext = new ClientProductViewModel(client);
    }
}