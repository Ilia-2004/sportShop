using sportShop.EntityFramework;
using sportShop.EntityFramework.Models;
using sportShop.Views.GeneralPages;
using AuthorizationView = sportShop.MVVM.Views.GeneralViews.AuthorizationView;

namespace sportShop;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        MainFrame.NavigationService.Navigate(new AuthorizationView());
        AddSDf();
        AddMembers();
        ASDASD();
    }

    public void AddMembers()
    {
        var dbContext = new Context();
    
        dbContext.ProductTypes.Add(new ProductTypes() { Name = "Tapok" });
        dbContext.SaveChanges();
    }
    //
    private void AddSDf()
    {
        var dbContext = new Context();
    
        dbContext.Administrators.Add(new Administrator() { Login = "Admin", Password = "Admin" });
        dbContext.SaveChanges();
    }
    //
    private void ASDASD()
    {
        var dbContext = new Context();
    
        dbContext.Fabrics.Add(new Fabric() { Name = "Fabrik" });
        dbContext.SaveChanges();
    }
}