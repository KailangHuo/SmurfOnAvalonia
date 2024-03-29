﻿using System.Collections.Generic;

namespace SMURF_Ava.ViewModels;

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
}