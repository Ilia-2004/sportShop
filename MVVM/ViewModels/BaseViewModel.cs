using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace sportShop.MVVM.ViewModels;

/// <summary>
/// Реализация интерфейса INotifyPropertyChanged для ViewModel 
/// </summary>
public class BaseViewModel : INotifyPropertyChanged
{
  /* Реализация события INotifyPropertyChanged */
  public event PropertyChangedEventHandler? PropertyChanged;

  /// <summary>
  /// Реализация метода INotofyPropertyChanged
  /// </summary>
  /// <param name="propertyName"></param>
  protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}