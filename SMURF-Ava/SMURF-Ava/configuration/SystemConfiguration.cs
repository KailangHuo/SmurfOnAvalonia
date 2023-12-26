﻿using System.Collections;
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

    private XmlDocument _document;

    #endregion
    

    private void Init() {
        ConfigurationFilePath = System.Environment.CurrentDirectory + @"/Configuration/Configuration.xml";
        _document = new XmlDocument();
        _document.Load(ConfigurationFilePath);
        InitApplicationList();
        InitCmdlineCommandsList();
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
        XmlNode constantAppListNode = _document.SelectSingleNode(@"/Root/CmdlineCommands");
        XmlNodeList childNodes = constantAppListNode.ChildNodes;
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

    public List<string> GetAppList() {
        return this.applicationList;
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



}