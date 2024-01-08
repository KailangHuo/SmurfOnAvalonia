using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using EventDrivenElements;
using SMURF_Ava.Models;
using SMURF_Ava.Views;

namespace SMURF_Ava.ViewModels;

public class PopupManager_ViewModel : AbstractEventDrivenViewModel{

    public PopupManager_ViewModel() {
        this.DefaultMainWindow = SystemFacade.GetInstance().MainWindow;
        this.RunningCommandStack = new List<UIH_Command>();
        this._locker = new object();
        IsBusy = false;
        Notification = "";
        ExceptionManager.GetInstance().RegisterObserver(this);
    }

    #region NOTIFIABLE_PROPERTIES

    private bool _isBusy;

    public bool IsBusy {
        get {
            return _isBusy;
        }
        set {
            if (_isBusy == value) return;
            _isBusy = value;
            RisePropertyChanged(nameof(IsBusy));
        }
    }

    private string _notification;

    public string Notification {
        get {
            return _notification;
        }
        set {
            if(_notification == value)return;
            _notification = value;
            RisePropertyChanged(nameof(Notification));
        }
    }

    #endregion

    #region COMMANDS

    public void CancelCurrentCommand() {
        if(RunningCommandStack.Count == 0)return;
        lock (_locker) {
            if(RunningCommandStack.Count == 0)return;
            UIH_Command uihCommand = RunningCommandStack[0];
            uihCommand.Cancel();
            RemoveRunningCommand(uihCommand);
        }
    }

    #endregion


    private object _locker;
    
    private Window DefaultMainWindow;

    private List<UIH_Command> RunningCommandStack;

    private void PushRunningCommand(UIH_Command uihCommand) {
        lock (_locker) {
            RunningCommandStack.Insert(0, uihCommand);
            UpdateTopCommand();
        }
    }

    private void RemoveRunningCommand(UIH_Command uihCommand) {
        lock (_locker) {
            if(!this.RunningCommandStack.Contains(uihCommand)) return;
            this.RunningCommandStack.Remove(uihCommand);
            UpdateTopCommand();
        }
    }

    private void UpdateTopCommand() {
        if (RunningCommandStack.Count == 0) {
            IsBusy = false;
            Notification = "";
            return;
        }
        UIH_Command uihCommand = RunningCommandStack[0];
        IsBusy = true;
        Notification = uihCommand.CommandName + " is processing...";
    }

    private void PopupExceptionWindow(string content) {
        PopupWindow window = new PopupWindow(content);
        window.ShowDialog(this.DefaultMainWindow);
    }
    
    public override void UpdateByEvent(string propertyName, object o) {
        Dispatcher.UIThread.Invoke(() => {
            if (propertyName.Equals(nameof(CommandLineCommunicator.PublishCommandStart))) {
                UIH_Command command = (UIH_Command)o;
                PushRunningCommand(command);
                return;
            }

            if (propertyName.Equals(nameof(CommandLineCommunicator.PublishCommandFinished))) {
                UIH_Command command = (UIH_Command)o;
                RemoveRunningCommand(command);
                return;
            }

            if (propertyName.Equals(nameof(ExceptionManager.ThrowException))) {
                this.PopupExceptionWindow((string)o);
                return;
            }
        });
    }
}