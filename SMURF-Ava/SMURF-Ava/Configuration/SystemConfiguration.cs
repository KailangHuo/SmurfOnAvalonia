using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using DynamicData;

namespace SMURF_Ava.configuration;

public class SystemConfiguration {
    private static SystemConfiguration _instance;

    public static SystemConfiguration GetInstance() {
        if (_instance == null) {
            lock (typeof(SystemConfiguration)) {
                if (_instance == null) {
                    _instance = new SystemConfiguration();
                }
            }
        }

        return _instance;
    }

    private SystemConfiguration() {
        Init();
    }

    #region PROPERTIES

    private string ConfigurationFilePath;

    private List<string> applicationList;

    private OrderedDictionary CmdlineCommandsMap;

    private Dictionary<string, string> CmdResultMap;

    private List<string> LangNameList;

    private Dictionary<string, string> LanguageConfigMap;

    private XmlDocument _document;

    #endregion
    

    private void Init() {
        ConfigurationFilePath = System.Environment.CurrentDirectory + @"/Configuration/Configuration.xml";
        _document = new XmlDocument();
        _document.Load(ConfigurationFilePath);
        InitApplicationList();
        InitCmdlineCommandsList();
        InitCmdlineResultMap();
        InitLanguageConfigMap();
    }

    private void InitApplicationList() {
        applicationList = new List<string>();
        XmlNode constantAppListNode = _document.SelectSingleNode(@"/Root/Applications");
        XmlNodeList childNodes = constantAppListNode.ChildNodes;
        foreach (XmlNode node in childNodes) {
            XmlAttributeCollection attributeCollection = node.Attributes;
            foreach (XmlAttribute  xmlAttribute in attributeCollection) {
                if (xmlAttribute.Name == "Name") {
                    applicationList.Add(xmlAttribute.Value);
                }
            }
        }
    }

    private void InitCmdlineCommandsList() {
        this.CmdlineCommandsMap = new OrderedDictionary();
        XmlNode CmdlineCommandNode = _document.SelectSingleNode(@"/Root/CmdlineCommands");
        XmlNodeList childNodes = CmdlineCommandNode.ChildNodes;
        foreach (XmlNode node in childNodes) {
            List<string> list = new List<string>();
            XmlAttributeCollection attributeCollection = node.Attributes;
            foreach (XmlAttribute  xmlAttribute in attributeCollection) {
                if (xmlAttribute.Name == "CommandName") {
                    this.CmdlineCommandsMap.Add(xmlAttribute.Value, list);
                }

                if (xmlAttribute.Name == "CommandParameterNames") {
                    string[] cmdNameArr = xmlAttribute.Value.Split(",");
                    for (int i = 0; i < cmdNameArr.Length; i++) {
                        list.Add(cmdNameArr[i]);
                    }
                }
            }
        }
    }

    private void InitCmdlineResultMap() {
        this.CmdResultMap = new Dictionary<string, string>();
        XmlNode commandResultNode = _document.SelectSingleNode(@"/Root/CmdlineCommandResultCodes");
        XmlNodeList childNodes = commandResultNode.ChildNodes;
        foreach (XmlNode node in childNodes) {
            string indexStr = "";
            string interpretationStr = "";
            XmlAttributeCollection attributeCollection = node.Attributes;
            foreach (XmlAttribute xmlA in attributeCollection) {
                
                if (xmlA.Name == "Index") {
                    indexStr = xmlA.Value;
                    continue;
                }

                if (xmlA.Name == "Interpretation") {
                    interpretationStr = xmlA.Value;
                    this.CmdResultMap.Add(indexStr, interpretationStr);
                }
            }
        }
    }

    private void InitLanguageConfigMap() {
        this.LangNameList = new List<string>();
        this.LanguageConfigMap = new Dictionary<string, string>();
        XmlNode commandResultNode = _document.SelectSingleNode(@"/Root/LanguageList");
        XmlNodeList childNodes = commandResultNode.ChildNodes;
        foreach (XmlNode node in childNodes) {
            string langName = "";
            string langValue = "";
            XmlAttributeCollection attributeCollection = node.Attributes;
            foreach (XmlAttribute xmlAttribute in attributeCollection) {
                if (xmlAttribute.Name == "Name") {
                    langName = xmlAttribute.Value;
                    this.LangNameList.Add(langName);
                    continue;
                }

                if (xmlAttribute.Name == "Value") {
                    langValue = xmlAttribute.Value;
                    this.LanguageConfigMap.Add(langName, langValue);
                }
            }
        }
    }

    public List<string> GetAppList() {
        return this.applicationList;
    }

    public List<string> GetLangNameList() {
        return this.LangNameList;
    }

    public List<string> GetCommandParamsByName(string commandName) {
        List<string> list = new List<string>();
        List<string> tempList = (List<string>)this.CmdlineCommandsMap[commandName];
        for (int i = 0; i < tempList.Count; i++) {
            list.Add(tempList[i]);
        }

        return list;
    }

    public List<string> GetCmdlineCommandNameList() {
        List<string> nameList = new List<string>();
        foreach (DictionaryEntry dictionaryEntry in this.CmdlineCommandsMap) {
            nameList.Add((string)dictionaryEntry.Key);
        }

        return nameList;
    }

    public string GetCmdlineResultByCodeStr(string codeStr) {
        if (!this.CmdResultMap.ContainsKey(codeStr)) return "UNDEFINED CODE";
        return this.CmdResultMap[codeStr];
    }

    public string GetLanguageValueByName(string langName) {
        if (!this.LanguageConfigMap.ContainsKey(langName)) return "UNDEFINED LANGUAGE";
        return this.LanguageConfigMap[langName];
    }

}