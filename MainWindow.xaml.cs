using sportShop.Pages.GeneralPages;

namespace sportShop;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        MainFrame.NavigationService.Navigate(new AuthorizationView());
        //AddSDf();
    }

    // public void AddMembers()
    // {
    //     var dbContext = new DBContext();
    //
    //     dbContext.ProductTypes.Add(new ProductTypes() {Name = "Tapok"});
    //     dbContext.SaveChanges();
    // }
    //
    // private void AddSDf()
    // {
    //     var dbContext = new DBContext();
    //
    //     dbContext.Managers.Add(new Manager() {Login = "Manager", Password = "Manager"});
    //     dbContext.SaveChanges();
    // }
    //
    // private void ASDASD()
    // {
    //     var dbContext = new DBContext();
    //
    //     dbContext.Fabrics.Add(new Fabric() {Name = "Fabrik"});
    //     dbContext.SaveChanges();
    // }
}