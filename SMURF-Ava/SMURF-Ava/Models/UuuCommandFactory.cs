using System.Collections.Generic;
using System.Reflection;
using EventDrivenElements;
using SMURF_Ava.configuration;
using SMURF_Ava.ViewModels;

namespace SMURF_Ava.Models;

public class UuuCommandFactory {
    
    private static UuuCommandFactory _instance;

    private UuuCommandFactory() {
        
    }

    public static UuuCommandFactory GetInstance() {
        if (_instance == null) {
            lock (typeof(UuuCommandFactory)) {
                if (_instance == null) {
                    _instance = new UuuCommandFactory();
                }
            }
        }

        return _instance;
    }

    public Uuu_Command CreateUCommand(string commandName, MetaDataObject metaDataObject, UuuCommandEnum commandEnum) {
        Uuu_Command uuuCommand = null;
        if (commandEnum == UuuCommandEnum.COMMAND_LINE) {
            uuuCommand = new Uuu_Command();
            uuuCommand.ClientPath = metaDataObject.clientPath;
            uuuCommand.CommandName = commandName;
            uuuCommand.UuuCommandEnum = commandEnum;
            uuuCommand.CommandBody = commandName;
            
            List<string> cmdParamsNames = SystemConfiguration.GetInstance().GetCommandParamsByName(uuuCommand.CommandName);
            HashSet<string> cmdParamsNameSet = new HashSet<string>(cmdParamsNames);
            FieldInfo[] fields = metaDataObject.GetType().GetFields();
            foreach (FieldInfo fieldInfo in fields) {
                if (cmdParamsNameSet.Contains(fieldInfo.Name)) {
                    string fieldValue = (string)fieldInfo.GetValue(metaDataObject);
                    if(string.IsNullOrEmpty(fieldValue))continue;
                    uuuCommand.CommandBody += " --"
                                          + fieldInfo.Name
                                          + "="
                                          + fieldValue;
                }
            }
        }

        return uuuCommand;
    }
}