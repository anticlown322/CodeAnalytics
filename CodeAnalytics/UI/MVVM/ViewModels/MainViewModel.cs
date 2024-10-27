using System.Windows;
using UI.Core;
using UI.MVVM.Models;

namespace UI.MVVM.ViewModels;

public class MainViewModel : ObservableObject
{
    //view models
    public HasltedViewModel HasltedVm { get; set; }
    public JilbaViewModel JilbaVm { get; set; }

    //top menu commands
    public RelayCommand MinimizeWindowCommand { get; set; }
    public RelayCommand CloseWindowCommand { get; set; }
    
    //left menu commands
    public RelayCommand HalstedViewCommand { get; set; }
    public RelayCommand JilbaViewCommand { get; set; }

    //current view
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
        HasltedVm = new HasltedViewModel();
        JilbaVm = new JilbaViewModel();

        CurrentView = HasltedVm; //HasltedVm = homepage
        
        //top menu
        MinimizeWindowCommand = new RelayCommand(MinimizeWindow);
        CloseWindowCommand = new RelayCommand(CloseWindow);
        HalstedViewCommand = new RelayCommand(obj => CurrentView = HasltedVm );
        JilbaViewCommand = new RelayCommand(obj => CurrentView = JilbaVm );
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