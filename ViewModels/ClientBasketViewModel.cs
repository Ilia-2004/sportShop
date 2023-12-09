using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using sportShop.Models;
using sportShop.Pages.ClientPages;

namespace sportShop.ViewModels;

public class ClientBasketViewModel : INotifyPropertyChanged
{
    private DbContext _dbContext;

    private ObservableCollection<Product>? _products;

    public ObservableCollection<Product>? Products
    {
        get => _products;
        set
        {
            _products = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Products)));
        }
    }

    private Client _client;

    public Client Client
    {
        get => _client;
        set
        {
            _client = value;
            Products = new ObservableCollection<Product>(_client.Products);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Client)));
        }
    }

    public RelayCommand<Product> DeleteProductCommand { get; private set; }
    public RelayCommand NavigateClientProductPage { get; private set; }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ClientBasketViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        _dbContext = new DbContext();
        if (_client != null)
            _products = new ObservableCollection<Product>(_client.Products);


        DeleteProductCommand = new RelayCommand<Product>(DeleteProductCommandExecute);
        NavigateClientProductPage = new RelayCommand(NavigateClientProductPageExecute);
    }

    private void NavigateClientProductPageExecute()
    {
        var mainWindow = Application.Current.MainWindow as MainWindow;
        mainWindow?.MainFrame.NavigationService.Navigate(
            new ClientProductView(_dbContext.Clients.First(client => client.Id == Client.Id)));
    }

    private void DeleteProductCommandExecute(Product product)
    {
        _client.Products.Remove(product);
        _products = new ObservableCollection<Product>(_client.Products);

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Products)));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}