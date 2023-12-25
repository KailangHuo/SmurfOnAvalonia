using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using EventDrivenElements;
using SMURF_Ava.configuration;
using SMURF_Ava.Models;
using SMURF_Ava.Views;

namespace SMURF_Ava.ViewModels;

public class MainWindowViewModel : AbstractEventDrivenViewModel {

    #region CONSTRUCTION

    public MainWindowViewModel(MetaDataObject metaDataObject = null) {
        this._metaDataObject = new MetaDataObject();
        this.UserAccountObject = new UserAccountViewModel();
        Init();
    }

    private void Init() {
        this.SystemLogManagerViewModel = new SystemLogManager_ViewModel();
        this.PopupManagerViewModel = new PopupManager_ViewModel();
        this.ApplicationList = new ObservableCollection<string>(SystemConfiguration.GetInstance().GetAppList());
        this.SelectedAppName = ApplicationList[0];
    }
    
    #endregion

    #region PROPERTIES

    private MetaDataObject _metaDataObject;

    #endregion

    #region NOTIFIABLE_PROPERTIES
    
    public ObservableCollection<string> ApplicationList { get; private set; }

    public SystemLogManager_ViewModel SystemLogManagerViewModel { get; private set; }

    public PopupManager_ViewModel PopupManagerViewModel { get; private set; }

    private UserAccountViewModel _userAccountViewModel;
    
    public UserAccountViewModel UserAccountObject {
        get {
            return _userAccountViewModel;
        }
        private set {
            if (_userAccountViewModel == value) return;
            _userAccountViewModel = value;
            this._metaDataObject.UserAccountViewModel = value;
            this.Saved = false;
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
            this._metaDataObject.ClientPath = value;
            this.Saved = false;
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
            this._metaDataObject.SelectedApplication = value;
            this.Saved = false;
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
            this._metaDataObject.StudyUid = value;
            this.Saved = false;
            RisePropertyChanged(nameof(StudyUid));
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
    

    #endregion

    #region COMMANDS

    public void CallUpCommand(object o = null) {
        SystemFacade.GetInstance().InvokeCommand("CALL-UP", _metaDataObject);
    }

    public void VerticalLoginCommand(object o = null) {
        SystemFacade.GetInstance().InvokeCommand("Vertical CALL-UP", _metaDataObject);
    }

    public void SaveCommand(object o = null) {
        this.Saved = true;
        SystemFacade.GetInstance().InvokeCommand("Save", _metaDataObject);
    }

    public void CloseClientCommand(object o = null) {
        SystemFacade.GetInstance().InvokeCommand("CLOSE CLIENT", _metaDataObject);
    }

    public void ClearLogCommand() {
        SystemFacade.GetInstance().InvokeCommand("CLEAR LOG", _metaDataObject);
    }

    public void OpenInCmdCommand() {
        Process.Start("cmd.exe", "/k cd " + this.ClientPath);
    }

    #endregion


    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(SystemFacade.PublishCmdCommunicator))) {
            CommandLineCommunicator commandLineCommunicator = (CommandLineCommunicator)o;
            commandLineCommunicator.RegisterObserver(this.PopupManagerViewModel);
            return;
        }

        if (propertyName.Equals(nameof(SystemFacade.PublishSystemLogger))) {
            SystemLogger systemLogger = (SystemLogger)o;
            systemLogger.RegisterObserver(this.SystemLogManagerViewModel);
            return;
        }
    }
}