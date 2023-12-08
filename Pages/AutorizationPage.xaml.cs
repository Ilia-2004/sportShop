using System.Linq;
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
        private readonly DBContext _dbContext;
        #endregion

        /// <summary>
        /// метод окна
        /// </summary>
        public AutorizationPage()
        {
            _dbContext = new DBContext();
            InitializeComponent();
        }

        /* метод для кнопки входа */
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var app = (App)Application.Current;

            if (_dbContext.Clients.Any(client => client.Password == Password.Password && client.Login == Login.Text))
                app.IsAdminLogged = true;

            if (_dbContext.Managers.Any(manager => manager.Password == Password.Password && manager.Login == Login.Text))
                app.IsManagerLogged = true;

            if (_dbContext.Administraitors.Any(administraitor => administraitor.Password == Password.Password && administraitor.Login == Login.Text))
                app.IsAdminLogged = true;
            
            else
                MessageBox.Show("", "Ti dolbaeb!");
        }
    }
}
