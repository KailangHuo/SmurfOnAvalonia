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
        this.commandPopupMap = new Dictionary<UIH_Command, InProgressWindow>();
        ExceptionManager.GetInstance().RegisterObserver(this);
    }

    private Window DefaultMainWindow;

    private Dictionary<UIH_Command, InProgressWindow> commandPopupMap;

    private void PopupExceptionWindow(string content) {
        ExceptionWindow window = new ExceptionWindow(content);
        window.ShowDialog(this.DefaultMainWindow);
    }

    private void PopupCommandProcessing_BlockWindow(UIH_Command uihCommand) {
        if(commandPopupMap.ContainsKey(uihCommand)) return;
        InProgressWindow inProgressWindow = new InProgressWindow(uihCommand.CommandName);
        commandPopupMap.Add(uihCommand, inProgressWindow);
        inProgressWindow.ShowDialog(this.DefaultMainWindow);
    }

    private void CloseCommandProcessing_BlockWindow(UIH_Command uihCommand) {
        if (commandPopupMap.ContainsKey(uihCommand)) {
            InProgressWindow progressWindow = commandPopupMap[uihCommand];
            //Dispatcher.UIThread.Invoke(() => { } );
            progressWindow.Close();
            commandPopupMap.Remove(uihCommand);
        }

    }

    public override void UpdateByEvent(string propertyName, object o) {
        Dispatcher.UIThread.Invoke(() => {
            if (propertyName.Equals(nameof(CommandLineCommunicator.PublishCommandStart))) {
                UIH_Command command = (UIH_Command)o;
                PopupCommandProcessing_BlockWindow(command);
                return;
            }

            if (propertyName.Equals(nameof(CommandLineCommunicator.PublishCommandFinished))) {
                UIH_Command command = (UIH_Command)o;
                CloseCommandProcessing_BlockWindow(command);
                return;
            }

            if (propertyName.Equals(nameof(ExceptionManager.ThrowException))) {
                this.PopupExceptionWindow((string)o);
                return;
            }
        });
    }
}