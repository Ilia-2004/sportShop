using System.Linq;
using System.Windows;
using sportShop.EntityFramework.Models;
using sportShop.MVVM.ViewModels.ProductViewModels;
using sportShop.Views.AdminPages;
using AdminRegistrationView = sportShop.MVVM.Views.AdminViews.AdminRegistrationView;

namespace sportShop.MVVM.ViewModels.AdminViewModels;

public class AdminProductViewModel : ProductViewModel
{
    public RelayCommand AddProductCommand { get; private set; }
    public RelayCommand NavigateAdminRegistrationPage { get; private set; }
    public RelayCommand<Product> DeleteProductCommand { get; private set; }

    public AdminProductViewModel()
    {
        NavigateAdminRegistrationPage = new RelayCommand(NavigateAdminRegistrationPageExecute);
        AddProductCommand = new RelayCommand(AddProductCommandExecute);
        DeleteProductCommand = new RelayCommand<Product>(DeleteProductCommandExecute);
    }

    private void NavigateAdminRegistrationPageExecute()
    {
        Context.SaveChanges();

        var mainWindow = Application.Current.MainWindow as MainWindow;
        mainWindow?.MainFrame.NavigationService.Navigate(new AdminRegistrationView());
    }

    private void DeleteProductCommandExecute(Product product)
    {
        Context.Products.Remove(product);

        Products.Remove(product);
    }

    private void AddProductCommandExecute()
    {
        var product = new Product
        {
            Name = $"Product{Products.Count}", ProductTypeId = Context.ProductTypes.First().Id,
            ProductType = Context.ProductTypes.First(),
            Fabric = Context.Fabrics.First(), FabricId = Context.Fabrics.First().Id, Price = 0, Sale = 0, ProductCount = 1
        };

        var fabric = Context.Fabrics.Find(product.FabricId);

        fabric?.Products.Add(product);
        Context.Products.Add(product);

        Products.Add(product);
    }
}