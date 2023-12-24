using sportShop.ViewModels;

namespace sportShop.Pages.AdminPages;

public partial class AdminRegistrationView
{
    public AdminRegistrationView()
    {
        DataContext = new AdminRegistrationViewModel();

        InitializeComponent();
    }
}