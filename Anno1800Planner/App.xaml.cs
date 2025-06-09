using Anno1800Planner.Models;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Anno1800Planner;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        GameData.Game.Initialize();
    }
    protected override void OnExit(ExitEventArgs e)
    {
        if (Database.Instance.HasUnsavedChanges)
        {
            var result = MessageBox.Show("You have unsaved changes. Save before exit?", "Exit", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Cancel)
            {
                e.ApplicationExitCode = 1;
                return;
            }
            else if (result == MessageBoxResult.Yes)
            {
                Database.Instance.Save();
            }
        }

        base.OnExit(e);
    }
}

