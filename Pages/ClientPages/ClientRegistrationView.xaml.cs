using sportShop.ViewModels;

namespace sportShop.Pages.ClientPages;

public partial class ClientRegistrationView
{
    public ClientRegistrationView()
    {
        InitializeComponent();

        DataContext = new RegistrationViewModel();
    }
}