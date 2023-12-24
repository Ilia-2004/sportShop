using sportShop.ViewModels;

namespace sportShop.Pages.ManagerPages;

public partial class ManagerProductView
{
    public ManagerProductView()
    {
        DataContext = new ProductViewModel();

        InitializeComponent();
    }
}