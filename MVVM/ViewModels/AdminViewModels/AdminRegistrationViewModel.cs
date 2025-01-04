using System.Windows;
using sportShop.EntityFramework;
using sportShop.EntityFramework.Models;
using sportShop.MVVM.RelayCommands;
using sportShop.MVVM.Views;
using AdminProductView = sportShop.MVVM.Views.AdminViews.AdminProductView;

namespace sportShop.MVVM.ViewModels.AdminViewModels;

/// <summary>
/// Реализация ViewModel для администратора
/// </summary>
public sealed class AdminRegistrationViewModel : BaseViewModel
{
  #region Свойства

  private readonly Context _context;

  private string _login;

  public string Login
  {
    get => _login;
    set
    {
      _login = value;
      OnPropertyChanged(nameof(Login));
    }
  }

  private string _password;

  public string Password
  {
    get => _password;
    set
    {
      _password = value;
      OnPropertyChanged(nameof(Password));
    }
  }

  private string _submitPassword;

  public string SubmitPassword
  {
    get => _submitPassword;
    set
    {
      _submitPassword = value;
      OnPropertyChanged(nameof(SubmitPassword));
    }
  }

  #endregion

  #region Свойства команд

  public RelayCommand RegistrationManagerCommand { get; set; }
  public RelayCommand RegistrationAdministratorCommand { get; set; }
  public RelayCommand NavigateToAdminProductPage { get; set; }

  #endregion

  #region Реализация команд

  /// <summary>
  /// Конструктор по умолчанию
  /// </summary>
  public AdminRegistrationViewModel()
  {
    _context = new Context();

    _password = string.Empty;
    _login = string.Empty;
    _submitPassword = string.Empty;

    RegistrationManagerCommand = new RelayCommand(RegistrationManagerCommandExecute);
    RegistrationAdministratorCommand = new RelayCommand(RegistrationAdministratorCommandExecute);
    NavigateToAdminProductPage = new RelayCommand(NavigateToAdminProductPageCommandExecute);
  }

  /// <summary>
  /// Команда навигации 
  /// </summary>
  private void NavigateToAdminProductPageCommandExecute()
  {
    _context.SaveChanges();

    var mainWindow = Application.Current.MainWindow as MainView;
    mainWindow?.MainFrame.NavigationService.Navigate(new AdminProductView());
  }

  /// <summary>
  /// Команда регистрации администратора
  /// </summary>
  private void RegistrationAdministratorCommandExecute()
  {
    _context.Administrators.Add(new Administrator { Login = _login, Password = _password });

    MessageBox.Show("Администратор зарегистрирован!");
  }

  /// <summary>
  /// Команда регистрации менеджера
  /// </summary>
  private void RegistrationManagerCommandExecute()
  {
    _context.Managers.Add(new Manager() { Login = _login, Password = _password });

    MessageBox.Show("Менеджер зарегистрирован!");
  }

  #endregion
}