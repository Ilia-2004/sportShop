using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace sportShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class AutorizationPage : Page
    {
        /// <summary>
        /// описание глобальных переменных и констант
        /// </summary>
        #region VariablesAndConstants
        private const string LineKey = "52";
        #endregion

        /// <summary>
        /// метод окна
        /// </summary>
        public AutorizationPage() => InitializeComponent();

        /* метод для кнопки входа */
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text == LineKey && Password.Password == LineKey)
            {
                var app = (App)Application.Current;
                app.IsAdminLogged = true;
                
                NavigationService.Navigate(new ProductsPage());
            }
            else
                MessageBox.Show("", "Ti dolbaeb!");
        }
    }
}
