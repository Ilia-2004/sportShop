using System.Windows;
using System.Windows.Media;

namespace sportShop.Pages
{
  /// <summary>
  /// Логика взаимодействия для Autorization.xaml
  /// </summary>
  public partial class Autorization : Window
  {
    /// <summary>
    /// описание глобальных переменных и констант
    /// </summary>
    #region VariablesAndConstants
    private const string LineKey = "admin";
    private MainWindow _mainWindow = new MainWindow();
    #endregion

    /// <summary>
    /// метод окна
    /// </summary>
    public Autorization() => InitializeComponent();

    /* метод для кнопки входа */
    private void Button_Click(object sender, RoutedEventArgs e)
    {
      if (Login.Text == LineKey && Password.Password == LineKey)
      {
        _mainWindow.Show();
        Close();
      }
      else
        if (Login.Text != LineKey && Password.Password != LineKey)
          Login.BorderBrush = Brushes.Red;
    }
  }
}
