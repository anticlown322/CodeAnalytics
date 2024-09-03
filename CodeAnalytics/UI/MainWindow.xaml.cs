using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    //pure UI logic and doesn't belong in a ViewModel so left it here
    private void DragWindow(object sender, MouseButtonEventArgs e)
    {
        if (e is { Source: Border, ChangedButton: MouseButton.Left })
        {
            DragMove();
        }
    }
}