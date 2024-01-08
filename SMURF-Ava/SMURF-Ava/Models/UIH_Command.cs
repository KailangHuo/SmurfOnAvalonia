using System.Diagnostics;
using System.Threading;

namespace SMURF_Ava.Models;

public class UIH_Command {

    public UihCommandEnum UihCommandEnum;

    public string ClientPath;

    public string CommandName;

    public string CommandHeader;

    public string UserName;

    public string Password;

    public string DomainUrl;

    public string StudyUid;

    public string ApplicationName;

    public string CommandBody;

    public Thread CurrentProcessingThread;

    public Process CurrentProcessingProcess;

    public void Cancel() {
        this.CurrentProcessingThread.
        this.CurrentProcessingThread.Join();
        this.CurrentProcessingProcess.Kill();
    }

}

public enum UihCommandEnum {
    COMMAND_LINE,
    WEBSOCKET,
    RESTFUL
}