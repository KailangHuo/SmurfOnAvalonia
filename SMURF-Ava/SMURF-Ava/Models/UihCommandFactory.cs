using System.Collections.Generic;
using System.Reflection;
using EventDrivenElements;
using SMURF_Ava.configuration;
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
            uihCommand.ClientPath = metaDataObject.clientPath;
            uihCommand.CommandName = commandName;
            uihCommand.UihCommandEnum = commandEnum;
            uihCommand.CommandBody = commandName;
            
            List<string> cmdParamsNames = SystemConfiguration.GetInstance().GetCommandParamsByName(uihCommand.CommandName);
            HashSet<string> cmdParamsNameSet = new HashSet<string>(cmdParamsNames);
            FieldInfo[] fields = metaDataObject.GetType().GetFields();
            foreach (FieldInfo fieldInfo in fields) {
                if (cmdParamsNameSet.Contains(fieldInfo.Name)) {
                    string fieldValue = (string)fieldInfo.GetValue(metaDataObject);
                    if(string.IsNullOrEmpty(fieldValue))continue;
                    uihCommand.CommandBody += " --"
                                          + fieldInfo.Name
                                          + "="
                                          + fieldValue;
                }
            }
        }

        return uihCommand;
    }
}