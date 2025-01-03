using sportShop.MVVM.ViewModels.GeneralViewModels;

namespace sportShop.MVVM.Views.ClientViews;

public partial class ClientRegistrationView
{
    public ClientRegistrationView()
    {
        InitializeComponent();

        DataContext = new RegistrationViewModel();
    }
}