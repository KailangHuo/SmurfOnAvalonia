using System.Collections.Generic;
using System.Xml;

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

    private XmlDocument _document;

    #endregion
    

    private void Init() {
        applicationList = new List<string>();
        ConfigurationFilePath = System.Environment.CurrentDirectory + @"/Configuration/Configuration.xml";
        _document = new XmlDocument();
        _document.Load(ConfigurationFilePath);
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

    public List<string> GetAppList() {
        return this.applicationList;
    }



}