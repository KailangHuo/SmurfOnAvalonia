namespace SMURF_Ava.Models;

public class UIH_Command {

    public UihCommandType UihCommandType;

    public string CommandName;

    public string CommandContent;

}

public enum UihCommandType {
    COMMAND_LINE,
    WEBSOCKET,
    RESTFUL
}