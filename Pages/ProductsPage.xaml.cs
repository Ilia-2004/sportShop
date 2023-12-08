using sportShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static sportShop.DBContext;

namespace sportShop.Pages
{
    public partial class ProductsPage : Page
    {
        private readonly ApplicationContext _dbContext;
        private List<Product> products;

        public ProductsPage()
        {
            _dbContext = new ApplicationContext();

            InitializeComponent();
            SetAccessToDataGrid();

            products = _dbContext.Products.ToList();

            dataGrid.ItemsSource = products;
        }

        private void SetAccessToDataGrid()
        {
            var app = (App)Application.Current;           
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            _dbContext.Products.Add(new Product() { Name = $"Product{_dbContext.Products.Count()}", Type = "None" });
           
            SaveDbAndRefreshGrid();
        }

        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            List<object> selectedItems = new();

            foreach (var item in dataGrid.Items)
            {
                var row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(item);
                if (row == null || dataGrid.Columns[0] is not DataGridCheckBoxColumn checkBox)
                    continue;

                var element = checkBox.GetCellContent(row);
                if (element is CheckBox { IsChecked: true })
                    selectedItems.Add(item);
            }

            selectedItems.ForEach(c =>
            {
                if (c is Product product)
                    _dbContext.Products.Remove(product);
            });
            SaveDbAndRefreshGrid();
        }

        private void SaveDbAndRefreshGrid()
        {
            _dbContext.SaveChanges();

            products = _dbContext.Products.ToList();
            dataGrid.ItemsSource = products;
            dataGrid.Items.Refresh();
        }
    }
}
