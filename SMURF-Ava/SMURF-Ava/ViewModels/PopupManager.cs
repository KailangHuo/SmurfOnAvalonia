using System.Diagnostics;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using EventDrivenElements;
using SMURF_Ava.Views;

namespace SMURF_Ava.ViewModels;

public class PopupManager {

    private static PopupManager _instance;

    private PopupManager() {
        IClassicDesktopStyleApplicationLifetime lifetime = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
        this.DefaultMainWindow = lifetime.MainWindow;
    }

    public static PopupManager GetInstance() {
        if (_instance == null) {
            lock (typeof(PopupManager)) {
                if (_instance == null) {
                    _instance = new PopupManager();
                }
            }
        }

        return _instance;
    }

    private Window DefaultMainWindow;

    public void PopupProcessingWaiting_Block(string msg = null, Window parentWindow = null) {
        if (parentWindow == null) {
            parentWindow = this.DefaultMainWindow;
        }
        
        PopupWindow popupWindow = new PopupWindow(msg);
        popupWindow.ShowDialog(parentWindow);
        
        Thread thread = new Thread(() => {
            Thread.Sleep(3000);
            Dispatcher.UIThread.InvokeAsync(() => {popupWindow.Close(); });
            
        });
        thread.Start();
        
        
        
    }


}