using EventDrivenElements;
using SMURF_Ava.ViewModels;

namespace SMURF_Ava.Models;

public class UihCommandFactory {
    
    private static UihCommandFactory _instance;

    private UihCommandFactory() {
        
    }

    public static UihCommandFactory GetInstance() {
        if (_instance == null) {
            lock (typeof(UihCommandFactory)) {
                if (_instance == null) {
                    _instance = new UihCommandFactory();
                }
            }
        }

        return _instance;
    }

    public UIH_Command CreateUihCommand(string commandName, MetaDataObject metaDataObject, UihCommandEnum commandEnum) {
        UIH_Command uihCommand = null;
        if (commandEnum == UihCommandEnum.COMMAND_LINE) {
            uihCommand = new UIH_Command();
            uihCommand.ClientPath = metaDataObject.ClientPath;
            uihCommand.CommandName = commandName;
            uihCommand.UihCommandEnum = commandEnum;
            uihCommand.UserName = metaDataObject.UserAccountViewModel.UserName;
            uihCommand.Password = metaDataObject.UserAccountViewModel.Password;
            uihCommand.DomainUrl = metaDataObject.UserAccountViewModel.DomainUrl;
            uihCommand.ApplicationName = metaDataObject.SelectedApplication;
            uihCommand.StudyUid = metaDataObject.StudyUid;
        }

        return uihCommand;
    }
}