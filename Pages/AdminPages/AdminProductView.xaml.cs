using sportShop.ViewModels;

namespace sportShop.Pages.AdminPages;

public partial class AdminProductView
{
    public AdminProductView()
    {
        DataContext = new AdminProductViewModel();

        InitializeComponent();
    }
}