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
        args += this.CommandName + " ";
        args += "--user=" + this.UserName + " ";
        args += "--password=" + this.Password + " ";
        args += "--selectedStudy=" + this.StudyUid + " ";
        args += "--application=" + this.ApplicationName + " ";
        args += "--serverDomain=" + this.DomainUrl + " ";
        return args;
    }

}

public enum UihCommandType {
    COMMAND_LINE,
    WEBSOCKET,
    RESTFUL
}