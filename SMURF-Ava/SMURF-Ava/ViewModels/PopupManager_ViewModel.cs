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
        this.RunningCommandStack = new List<Uuu_Command>();
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
            Uuu_Command uuuCommand = RunningCommandStack[0];
            uuuCommand.Cancel();
            RemoveRunningCommand(uuuCommand);
        }
    }

    #endregion


    private object _locker;
    
    private Window DefaultMainWindow;

    private List<Uuu_Command> RunningCommandStack;

    private void PushRunningCommand(Uuu_Command uuuCommand) {
        lock (_locker) {
            RunningCommandStack.Insert(0, uuuCommand);
            UpdateTopCommand();
        }
    }

    private void RemoveRunningCommand(Uuu_Command uuuCommand) {
        lock (_locker) {
            if(!this.RunningCommandStack.Contains(uuuCommand)) return;
            this.RunningCommandStack.Remove(uuuCommand);
            UpdateTopCommand();
        }
    }

    private void UpdateTopCommand() {
        if (RunningCommandStack.Count == 0) {
            IsBusy = false;
            Notification = "";
            return;
        }
        Uuu_Command uuuCommand = RunningCommandStack[0];
        IsBusy = true;
        Notification = uuuCommand.CommandName + " is processing...";
    }

    private void PopupExceptionWindow(string content) {
        NotificationWindow window = new NotificationWindow(content);
        window.ShowDialog(this.DefaultMainWindow);
    }

    public void PupupStringManagerWindow(StringItemManager_ViewModel stringItemManagerViewModel, string titleName) {
        StringItemManagementWindow window = new StringItemManagementWindow(stringItemManagerViewModel, titleName);
        window.ShowDialog(this.DefaultMainWindow);
    }

    public override void UpdateByEvent(string propertyName, object o) {
        Dispatcher.UIThread.Invoke(() => {
            if (propertyName.Equals(nameof(CommandLineCommunicator.PublishCommandStart))) {
                Uuu_Command command = (Uuu_Command)o;
                PushRunningCommand(command);
                return;
            }

            if (propertyName.Equals(nameof(CommandLineCommunicator.PublishCommandFinished))) {
                Uuu_Command command = (Uuu_Command)o;
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