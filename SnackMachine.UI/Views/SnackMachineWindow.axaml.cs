using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SnackMachine.UI.Views
{
    public partial class SnackMachineWindow : Window
    {
        public SnackMachineWindow()
        {
            InitializeComponent();
            var dataGridType = typeof(DataGrid); // HACK
            AvaloniaXamlLoader.Load(this);
        }
    }
}
