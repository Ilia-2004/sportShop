using System;
using System.ComponentModel;
using System.Windows;
using sportShop.Models;
using sportShop.Pages.GeneralPages;

namespace sportShop.ViewModels;

sealed public class RegistrationPageViewModel : INotifyPropertyChanged
{
    private string _name;

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
        }
    }

    private string _login;

    public string Login
    {
        get => _login;
        set
        {
            _login = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Login)));
        }
    }

    private string _age;

    public string Age
    {
        get => _age;
        set
        {
            _age = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Age)));
        }
    }

    private string _password;

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
        }
    }

    private string _submitPassword;

    public string SubmitPassword
    {
        get => _submitPassword;
        set
        {
            _submitPassword = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SubmitPassword)));
        }
    }

    public RelayCommand RegistrationClientCommand { get; }
    public RelayCommand NavigateToAuthorizationCommand { get; }

    public RegistrationPageViewModel()
    {
        _name = string.Empty;
        _login = string.Empty;
        _age = string.Empty;
        _password = string.Empty;
        _submitPassword = string.Empty;

        RegistrationClientCommand = new RelayCommand(RegistrationClientCommandExecute);
        NavigateToAuthorizationCommand = new RelayCommand(NavigateToAuthorizationCommandExecute);
    }
    private static void NavigateToAuthorizationCommandExecute()
    {
        var mainWindow = Application.Current.MainWindow as MainWindow;

        mainWindow?.MainFrame.NavigationService.Navigate(new AuthorizationView());
    }
    private void RegistrationClientCommandExecute()
    {
        var dbContext = new DbContext();

        var newClient = new Client
        {
            Name = _name,
            Login = _login,
            Age = Convert.ToInt32(_age),
            Password = _password
        };

        MessageBox.Show("Регистрация прошла успешно!");
        dbContext.Clients.Add(newClient);
        dbContext.SaveChanges();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}