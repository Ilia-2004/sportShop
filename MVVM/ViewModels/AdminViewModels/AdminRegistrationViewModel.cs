using System.Windows;
using sportShop.EntityFramework;
using sportShop.EntityFramework.Models;
using sportShop.Views.AdminPages;
using AdminProductView = sportShop.MVVM.Views.AdminViews.AdminProductView;

namespace sportShop.MVVM.ViewModels.AdminViewModels;

public sealed class AdminRegistrationViewModel : BaseViewModel
{
    private readonly Context _context;

    private string _login;

    public string Login
    {
        get => _login;
        set
        {
            _login = value;
            OnPropertyChanged();
        }
    }

    private string _password;

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
        }
    }

    private string _submitPassword;

    public string SubmitPassword
    {
        get => _submitPassword;
        set
        {
            _submitPassword = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand RegistrationManagerCommand { get; set; }
    public RelayCommand RegistrationAdministratorCommand { get; set; }
    public RelayCommand NavigateToAdminProductPage { get; set; }

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

    private void NavigateToAdminProductPageCommandExecute()
    {
        _context.SaveChanges();

        var mainWindow = Application.Current.MainWindow as MainWindow;
        mainWindow?.MainFrame.NavigationService.Navigate(new AdminProductView());
    }

    private void RegistrationAdministratorCommandExecute()
    {
        _context.Administrators.Add(new Administrator() {Login = _login, Password = _password});

        MessageBox.Show("Администартор зарегестрирован!");
    }

    private void RegistrationManagerCommandExecute()
    {
        _context.Managers.Add(new Manager() {Login = _login, Password = _password});

        MessageBox.Show("Менеджер зарегестрирован!");
    }
}