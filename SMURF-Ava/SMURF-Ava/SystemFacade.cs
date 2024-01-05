

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
        this.ExceptionManager = Models.ExceptionManager.GetInstance();
        this.TcpShortServer = TcpShortServer.GetInstance();
        this._responseItemManager = new ResponseItemManager();
        PublishResponseItemManager();
        this.SystemLogger = SystemLogger.GetInstance();
        PublishSystemLogger();
        this.CommandLineCommunicator = new CommandLineCommunicator();
        PublishCmdCommunicator();
        
        this.CommandLineCommunicator.RegisterObserver(this.SystemLogger);
        this.TcpShortServer.RegisterObserver(this._responseItemManager);
        

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

    private ExceptionManager ExceptionManager;

    private SystemLogger SystemLogger;

    private CommandLineCommunicator CommandLineCommunicator;

    private TcpShortServer TcpShortServer;

    private ResponseItemManager _responseItemManager;

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
    
    public void PublishResponseItemManager() {
        PublishEvent(nameof(PublishResponseItemManager), this._responseItemManager);
    }

    public void Log(string content, LogTypeEnum logType = LogTypeEnum.SYSTEM_NOTIFICATION) {
        this.SystemLogger.UpdateLog(content, logType);
    }

    public void InvokeRpcCommand(string cmdName, MetaDataObject metaDataObject) {
        UIH_Command uihCommand = UihCommandFactory.GetInstance()
            .CreateUihCommand(cmdName, metaDataObject, UihCommandEnum.COMMAND_LINE);
        this.CommandLineCommunicator.SendCommand(uihCommand);
    }

    public void ClearLogCommand() {
        this.SystemLogger.ClearAllLog();
    }

}