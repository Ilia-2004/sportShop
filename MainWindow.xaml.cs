using System.Windows;
using System.Collections.Generic;
using static sportShop.DBContext;
using sportShop.Models;
using System.Linq;

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

            List<Product> products = _db.Products.ToList();

            dataGrid.ItemsSource = products;
          
        }

      

    }

}
