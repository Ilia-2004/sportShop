using sportShop.MVVM.ViewModels.AdminViewModels;

namespace sportShop.MVVM.Views.AdminViews;

public partial class AdminRegistrationView
{
    public AdminRegistrationView()
    {
        DataContext = new AdminRegistrationViewModel();

        InitializeComponent();
    }
}