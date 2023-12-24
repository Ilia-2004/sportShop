using sportShop.ViewModels;

namespace sportShop.Pages.GeneralPages;

public partial class AuthorizationView
{
    public AuthorizationView()
    {
        DataContext = new AuthorizationViewModel();

        InitializeComponent();
    }
}