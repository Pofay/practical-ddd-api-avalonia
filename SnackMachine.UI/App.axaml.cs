using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using SnackMachine.Logic;
using SnackMachine.UI.ViewModels;
using SnackMachine.UI.Views;

namespace SnackMachine.UI
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Line below is needed to remove Avalonia data validation.
                // Without this line you will get duplicate validations from both Avalonia and CT
                ExpressionObserver.DataValidators.RemoveAll(x => x is DataAnnotationsValidationPlugin);
                desktop.MainWindow = new SnackMachineWindow
                {
                    DataContext = new SnackMachineViewModel(new SnackMachineEntity()),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
