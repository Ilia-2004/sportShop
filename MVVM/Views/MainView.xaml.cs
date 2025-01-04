using AuthorizationView = sportShop.MVVM.Views.GeneralViews.AuthorizationView;

namespace sportShop.MVVM.Views;

public partial class MainView
{
  public MainView()
  {
    InitializeComponent();

    MainFrame.NavigationService.Navigate(new AuthorizationView());
  }
}