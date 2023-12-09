using System.ComponentModel;
using System.Windows;
using sportShop.Models;
using sportShop.Pages.AdminPages;

namespace sportShop.ViewModels;

sealed public class AdminRegistrationViewModel : INotifyPropertyChanged
{
    private readonly DbContext _dbContext;

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

    public RelayCommand RegistrationManagerCommand { get; set; }
    public RelayCommand RegistrationAdministratorCommand { get; set; }
    public RelayCommand NavigateToAdminProductPage { get; set; }


    public AdminRegistrationViewModel()
    {
        _dbContext = new DbContext();

        _password = string.Empty;
        _login = string.Empty;
        _submitPassword = string.Empty;

        RegistrationManagerCommand = new RelayCommand(RegistrationManagerCommandExecute);
        RegistrationAdministratorCommand = new RelayCommand(RegistrationAdministratorCommandExecute);
        NavigateToAdminProductPage = new RelayCommand(NavigateToAdminProductPageCommandExecute);
    }

    private void NavigateToAdminProductPageCommandExecute()
    {
        var mainWindow = Application.Current.MainWindow as MainWindow;
        mainWindow?.MainFrame.NavigationService.Navigate(new AdminProductView());
    }

    private void RegistrationAdministratorCommandExecute()
    {
        _dbContext.Administrators.Add(new Administrator() {Login = _login, Password = _password});
        _dbContext.SaveChanges();

        MessageBox.Show("Администартор зарегестрирован!");
    }

    private void RegistrationManagerCommandExecute()
    {
        _dbContext.Managers.Add(new Manager() {Login = _login, Password = _password});
        _dbContext.SaveChanges();

        MessageBox.Show("Менеджер зарегестрирован!");
    }


    public event PropertyChangedEventHandler? PropertyChanged;
}