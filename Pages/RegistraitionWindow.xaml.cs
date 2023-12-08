using sportShop.Models;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace sportShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistraitionWindow.xaml
    /// </summary>
    public partial class RegistraitionWindow : Page
    {
        private readonly DBContext _dbContext;
        public RegistraitionWindow()
        {
            ChooseRoleToRegisrationCmbBx.ItemsSource = new List<string>() { "Client", "Administraitor", "Manager" };
            _dbContext = new DBContext();

            InitializeComponent();
        }       


        private void AddManagerButt_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _dbContext.Managers.Add(new Manager() { Login = LoginTxtBlck.Text, Password = PasswordBox.Password });
        }

        private void AddClientButt_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _dbContext.Clients.Add(new Client() { Name = NameTxtBlck.Text, Age= Convert.ToInt32(AgeTxtBlck.Text), Login = LoginTxtBlck.Text, Password = PasswordBox.Password });
        }

        private void AddAdministraitorButt_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _dbContext.Administraitors.Add(new Administraitor() { Login = LoginTxtBlck.Text, Password= PasswordBox.Password });
        }

        private void ChooseRoleToRegisrationCmbBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
