

using Avalonia.Controls;
using EventDrivenElements;
using SMURF_Ava.Models;
using SMURF_Ava.ViewModels;

namespace SMURF_Ava;

public class SystemFacade : AbstractEventDrivenObject{

    private static SystemFacade _instance;

    private SystemFacade() {
        InitSystem();
    }

    private void InitSystem() {
        this.SystemLogger = new SystemLogger();
        PublishSystemLogger();
        this.CommandLineCommunicator = new CommandLineCommunicator();
        PublishCmdCommunicator();
        
        this.CommandLineCommunicator.RegisterObserver(this.SystemLogger);
        
        

    }

    public static SystemFacade GetInstance() {
        if (_instance == null) {
            lock (typeof(SystemFacade)) {
                if (_instance == null) {
                    _instance = new SystemFacade();
                }
            }
        }

        return _instance;
    }

    public Window MainWindow { get; private set; }

    public AbstractEventDrivenViewModel MainWindowViewModel{ get; private set; }

    private SystemLogger SystemLogger;

    private CommandLineCommunicator CommandLineCommunicator;

    public void RegisterMainWindow(Window window) {
        this.MainWindow = window;
    }

    public void RegisterMainViewModel(AbstractEventDrivenViewModel viewModel) {
        this.MainWindowViewModel = viewModel;
        this.RegisterObserver(viewModel);
    }

    public void PublishCmdCommunicator() {
        PublishEvent(nameof(PublishCmdCommunicator), this.CommandLineCommunicator);
    }

    public void PublishSystemLogger() {
        PublishEvent(nameof(PublishSystemLogger), this.SystemLogger);
    }

    public void Log(string content, LogTypeEnum logType = LogTypeEnum.SYSTEM_NOTIFICATION) {
        this.SystemLogger.UpdateLog(content, logType);
    }

    public void InvokeCommand(string cmdName, MetaDataObject metaDataObject) {
        UIH_Command uihCommand = UihCommandFactory.GetInstance()
            .CreateUihCommand(cmdName, metaDataObject, UihCommandType.COMMAND_LINE);
        this.CommandLineCommunicator.SendCommand(uihCommand);
    }

}