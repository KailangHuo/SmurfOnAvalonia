using System.Diagnostics;
using System.Threading;

namespace SMURF_Ava.Models;

public class Uuu_Command {

    public UuuCommandEnum UuuCommandEnum;

    public string ClientPath;

    public string CommandName;

    public string CommandHeader;

    public string UserName;

    public string Password;

    public string DomainUrl;

    public string StudyUid;

    public string ApplicationName;

    public string CommandBody;
    
    public CancellationTokenSource CancellationTokenSource;
    

    public void Cancel() {
        this.CancellationTokenSource.Cancel();
    }

}

public enum UuuCommandEnum {
    COMMAND_LINE,
    WEBSOCKET,
    RESTFUL
}