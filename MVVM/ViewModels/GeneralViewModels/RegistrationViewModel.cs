using System;
using System.Windows;
using sportShop.EntityFramework;
using sportShop.EntityFramework.Models;
using sportShop.MVVM.RelayCommands;
using sportShop.MVVM.Views;
using AuthorizationView = sportShop.MVVM.Views.GeneralViews.AuthorizationView;

namespace sportShop.MVVM.ViewModels.GeneralViewModels;

/// <summary>
/// Реализация ViewModel регистрации
/// </summary>
public sealed class RegistrationViewModel : BaseViewModel
{
  #region Свойства

  private readonly Context _context;

  private string _name;

  public string Name
  {
    get => _name;
    set
    {
      _name = value;
      OnPropertyChanged(nameof(Name));
    }
  }

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

  private string _age;

  public string Age
  {
    get => _age;
    set
    {
      _age = value;
      OnPropertyChanged(nameof(Age));
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

  public RelayCommand RegistrationClientCommand { get; }
  public RelayCommand NavigateToAuthorizationCommand { get; }

  #endregion

  /// <summary>
  /// Конструктор по умолчанию
  /// </summary>
  public RegistrationViewModel()
  {
    _context = new Context();

    _name = string.Empty;
    _login = string.Empty;
    _age = string.Empty;
    _password = string.Empty;
    _submitPassword = string.Empty;

    RegistrationClientCommand = new RelayCommand(RegistrationClientCommandExecute);
    NavigateToAuthorizationCommand = new RelayCommand(NavigateToAuthorizationCommandExecute);
  }

  #region Реализация команд

  /// <summary>
  /// Команда навигации
  /// </summary>
  private void NavigateToAuthorizationCommandExecute()
  {
    _context.SaveChanges();

    var mainWindow = Application.Current.MainWindow as MainView;

    mainWindow?.MainFrame.NavigationService.Navigate(new AuthorizationView());
  }

  /// <summary>
  /// Команда регистрации
  /// </summary>
  private void RegistrationClientCommandExecute()
  {
    var newClient = new Client
    {
      Name = _name,
      Login = _login,
      Age = Convert.ToInt32(_age),
      Password = _password
    };

    _context.Clients.Add(newClient);

    MessageBox.Show("Регистрация прошла успешно!");
  }

  #endregion
}