using sportShop.MVVM.ViewModels.ProductViewModels;

namespace sportShop.MVVM.Views.ManagerViews;

public partial class ManagerProductView
{
    public ManagerProductView()
    {
        DataContext = new ProductViewModel();

        InitializeComponent();
    }
}