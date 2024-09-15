using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace UI.MVVM.Views;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();

        List<DataGridElem> elems = new()
        {
            new DataGridElem { Index = "1", Value = "hui", Amount = "2"},
            new DataGridElem { Index = "2", Value = "hui", Amount = "3"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "3", Value = "hui", Amount = "4"},
            new DataGridElem { Index = "4", Value = "hui", Amount = "5"}
        };

        OperatorsGrid.ItemsSource = elems;
    }

    //for tests only
    public class DataGridElem
    {
        public string Index { get; set; }
        public string Value { get; set; }
        public string Amount { get; set; }
    }
}