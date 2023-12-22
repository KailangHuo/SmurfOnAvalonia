using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EventDrivenElements;
using SMURF_Ava.configuration;

namespace SMURF_Ava.ViewModels;

public class MainWindowViewModel : AbstractEventDrivenViewModel {

    #region CONSTRUCTION

    public MainWindowViewModel(StorageDataObject storageDataObject = null) {
        this.LogSystemInfo("System init!");
        this.StorageDataObject = storageDataObject;
        Init();
    }

    private void Init() {
        this.ApplicationList = new ObservableCollection<string>(SystemConfiguration.GetInstance().GetAppList());
        this.SelectedAppName = ApplicationList[0];
    }

    #endregion

    #region PROPERTIES

    private StorageDataObject StorageDataObject;

    #endregion

    #region NOTIFIABLE_PROPERTIES
    
    public ObservableCollection<string> ApplicationList { get; private set; }

    private UserAccountViewModel _userAccountObject;

    public UserAccountViewModel UserAccountObject {
        get {
            return _userAccountObject;
        }
        set {
            if(_userAccountObject == value)return;
            _userAccountObject = value;
            RisePropertyChanged(nameof(UserAccountObject));
        }
    }

    private string _clientPath;

    public string ClientPath {
        get {
            return _clientPath;
        }
        set {
            if(_clientPath == value)return;
            _clientPath = value;
            RisePropertyChanged(nameof(ClientPath));
        }
    }

    private string _selectedAppName;

    public string SelectedAppName {
        get {
            return _selectedAppName;
        }
        set {
            if(_selectedAppName == value)return;
            _selectedAppName = value;
            RisePropertyChanged(nameof(SelectedAppName));
        }
    }

    private string _studyUid;

    public string StudyUid {
        get {
            return _studyUid;
        }
        set {
            if(_studyUid == value)return;
            _studyUid = value;
            RisePropertyChanged(nameof(StudyUid));
        }
    }

    private string _systemLog;

    public string SystemLog {
        get {
            return _systemLog;
        }
        set {
            if(_systemLog == value)return;
            _systemLog = value;
            RisePropertyChanged(nameof(SystemLog));
        }
    }

    private bool _saved;

    public bool Saved {
        get {
            return _saved;
        }
        set {
            if(_saved == value)return;
            _saved = value;
            RisePropertyChanged(nameof(Saved));
        }
    }


    #endregion

    #region METHODS

    public void LogSystemInfo(string s) {
        string timeStamp = DateTime.Now.ToString();
        SystemLog += "[" + timeStamp + "] " + s + "\n";
    }

    #endregion

}