using System.Windows;
using UI.Core;

namespace UI.MVVM.ViewModels;

public class MainViewModel : ObservableObject
{
    //view models
    public HomeViewModel HomeVm { get; set; }

    //top menu commands
    public RelayCommand MinimizeWindowCommand { get; set; }
    public RelayCommand CloseWindowCommand { get; set; }

    private object _currentView;

    public object CurrentView
    {
        get { return _currentView; }
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    public MainViewModel()
    {
        //viewModels init
        HomeVm = new HomeViewModel();

        CurrentView = HomeVm; //testerView = homepage
        
        //top menu
        MinimizeWindowCommand = new RelayCommand(MinimizeWindow);
        CloseWindowCommand = new RelayCommand(CloseWindow);
    }
    
    void MinimizeWindow(object parameter)
    {
        if (parameter is Window window)
        {
            window.WindowState = WindowState.Minimized;
        }
    }

    void CloseWindow(object parameter)
    {
        if (parameter is Window window)
        {
            window.Close();
        }
    }
}