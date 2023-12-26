using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using EventDrivenElements;
using SMURF_Ava.ViewModels;
using SMURF_Ava.Views;

namespace SMURF_Ava;

public partial class App : Application {
    public override void Initialize() {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted() {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            desktop.MainWindow = new MainWindow();
            SystemFacade.GetInstance().RegisterMainWindow(desktop.MainWindow);
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
            SystemFacade.GetInstance().RegisterMainViewModel(mainWindowViewModel);
            desktop.MainWindow.DataContext = mainWindowViewModel;
        }

        base.OnFrameworkInitializationCompleted();
    }
    
    
}