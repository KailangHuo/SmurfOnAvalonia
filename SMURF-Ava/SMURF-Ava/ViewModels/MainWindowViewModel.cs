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
        Init();
    }

    private void Init() {
        this.LogManagerViewModel = LogManager_ViewModel.GetInstance();
        this.PopupManagerViewModel = new PopupManager_ViewModel();
        this.ApplicationList = new ObservableCollection<string>(SystemConfiguration.GetInstance().GetAppList());
        this.CommandItemContainerViewModel = new CommandItemContainer_ViewModel();
        this.CommandItemContainerViewModel.RegisterObserver(this);
        
        this.SelectedAppName = ApplicationList[0];
    }
    
    #endregion

    #region PROPERTIES

    private MetaDataObject _metaDataObject;

    #endregion

    #region NOTIFIABLE_PROPERTIES
    
    public ObservableCollection<string> ApplicationList { get; private set; }

    public LogManager_ViewModel LogManagerViewModel { get; private set; }

    public PopupManager_ViewModel PopupManagerViewModel { get; private set; }

    public CommandItemContainer_ViewModel CommandItemContainerViewModel { get; private set; }

    private string _userName;

    public string UserName {
        get {
            return _userName;
        }
        set {
            if(_userName == value)return;
            _userName = value;
            this._metaDataObject.user = value;
            RisePropertyChanged(nameof(UserName));
        }
    }
    
    private string _password;

    public string Password {
        get {
            return _password;
        }
        set {
            if(_password == value)return;
            _password = value;
            this._metaDataObject.password = value;
            RisePropertyChanged(nameof(Password));
        }
    }
    
    private string _domainUrl;

    public string DomainUrl {
        get {
            return _domainUrl;
        }
        set {
            if(_domainUrl == value)return;
            _domainUrl = value;
            this._metaDataObject.serverDomain = value;
            RisePropertyChanged(nameof(DomainUrl));
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
            this._metaDataObject.clientPath = value;
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
            this._metaDataObject.application = value;
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
            this._metaDataObject.selectedStudy = value;
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

    public void LoginLaunchCommand(object o = null) {
        SystemFacade.GetInstance().InvokeCommand("loginLaunch", _metaDataObject);
    }

    public void VerticalLoginCommand(object o = null) {
        SystemFacade.GetInstance().InvokeCommand("verticalLogin", _metaDataObject);
    }

    public void SaveCommand(object o = null) {
        this.Saved = true;
        SystemFacade.GetInstance().InvokeCommand("save", _metaDataObject);
    }

    public void CloseClientCommand(object o = null) {
        SystemFacade.GetInstance().InvokeCommand("closeClient", _metaDataObject);
    }

    public void ClearLogCommand() {
        SystemFacade.GetInstance().ClearLogCommand();
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
            systemLogger.RegisterObserver(this.LogManagerViewModel);
            return;
        }

        if (propertyName.Equals(nameof(CommandItem_ViewModel.InvokeCommand))) {
            string commandName = (string)o;
            SystemFacade.GetInstance().InvokeCommand(commandName, _metaDataObject);
            return;
        }
    }
}