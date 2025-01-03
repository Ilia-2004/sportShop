using sportShop.MVVM.ViewModels.AdminViewModels;

namespace sportShop.MVVM.Views.AdminViews;

public partial class AdminProductView
{
    public AdminProductView()
    {
        DataContext = new AdminProductViewModel();

        InitializeComponent();
    }
}