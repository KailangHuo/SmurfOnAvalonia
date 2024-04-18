using System.Collections.Generic;

namespace SMURF_Ava.ViewModels;

/**
 * MetaDataObject
 * 这个类中的名称是直接放入到cmd 命令中的内容, 需要与integration所要求的命令名完全一致
 */



public class MetaDataObject {

    public MetaDataObject() {
        this.portNumber = 3506 + "";
    }

    public string clientPath;

    public string user;

    public string password;

    public string language;

    public string serverDomain;

    public string selectedStudy;

    public string StudyStringItemsJson;

    public string selectedSeries;
    
    public string SeriesStringItemsJson;

    public string application;

    public string showTimeStamp;

    public string portNumber;

    public string authType;

    public string aeNodeName;

    public string token;

    // TODO:option中叫什么?
    public string notificationUrl;
}