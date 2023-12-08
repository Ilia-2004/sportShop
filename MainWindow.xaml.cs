using sportShop.Pages;
using System.Windows;

namespace sportShop;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

       MainFrame.Navigate(new AutorizationPage());
    }
}
