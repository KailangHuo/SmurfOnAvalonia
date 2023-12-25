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

    public string GetIntegrationArgs() {
        return "";
    }

}

public enum UihCommandType {
    COMMAND_LINE,
    WEBSOCKET,
    RESTFUL
}