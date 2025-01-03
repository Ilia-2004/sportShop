using sportShop.MVVM.ViewModels.GeneralViewModels;

namespace sportShop.MVVM.Views.GeneralViews;

public partial class AuthorizationView
{
    public AuthorizationView()
    {
        DataContext = new AuthorizationViewModel();

        InitializeComponent();
    }
}