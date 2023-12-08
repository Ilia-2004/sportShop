using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using static sportShop.DBContext;

namespace sportShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ApplicationContext _db = new ApplicationContext();

        public MainWindow()
        {
            InitializeComponent();

        }
    }
}
