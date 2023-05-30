using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Shapes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using SnackMachine.Logic;
using SnackMachine.Logic.Persistence;
using SnackMachine.UI.ViewModels;
using SnackMachine.UI.Views;

namespace SnackMachine.UI
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            var dataGridType = typeof(DataGrid); // HACK
            AvaloniaXamlLoader.Load(this);
            DotNetEnv.Env.TraversePath().Load();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Line below is needed to remove Avalonia data validation.
                // Without this line you will get duplicate validations from both Avalonia and CT
                ExpressionObserver.DataValidators.RemoveAll(x => x is DataAnnotationsValidationPlugin);

                var snackMachineId = Guid.Parse("09213a9c-ff65-4b01-b7da-ac7a792b119e");
                var repository = new SnackMachineRepository(new DataContextFactory());
                SnackMachineEntity snackMachine;
                var existingSnackMachine = repository.GetById(snackMachineId);
                if (existingSnackMachine == null)
                {
                    existingSnackMachine = new SnackMachineEntity(snackMachineId);
                    existingSnackMachine.LoadSnacks(1, new SnackPile(Snack.Chocolate, 10, 3m));
                    existingSnackMachine.LoadSnacks(2, new SnackPile(Snack.Soda, 15, 2m));
                    existingSnackMachine.LoadSnacks(3, new SnackPile(Snack.Gum, 20, 1m));
                    repository.Save(existingSnackMachine);
                }
                snackMachine = existingSnackMachine;
                desktop.MainWindow = new SnackMachineWindow
                {
                    DataContext = new SnackMachineViewModel(snackMachine)
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
