namespace SMURF_Ava.Models;

public class UIH_Command {

    public UihCommandType UihCommandType;

    public string ClientPath;

    public string CommandName;

    public string UserName;

    public string Password;

    public string DomainUrl;

    public string StudyUid;

    public string ApplicationName;

    public string Content;

    public string GetIntegrationArgs() {
        string args = "";
        args += this.CommandName + "\n";
        args += "--user=" + this.UserName + "\n";
        args += "--password=" + this.Password + "\n";
        args += "--selectedStudy=" + this.StudyUid + "\n";
        args += "--application=" + this.ApplicationName + "\n";
        args += "--serverDomain=" + this.DomainUrl + "\n";
        return args;
    }

}

public enum UihCommandType {
    COMMAND_LINE,
    WEBSOCKET,
    RESTFUL
}