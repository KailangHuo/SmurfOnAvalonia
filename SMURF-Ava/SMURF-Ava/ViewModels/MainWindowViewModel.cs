﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using DynamicData;
using EventDrivenElements;
using Newtonsoft.Json;
using SMURF_Ava.configuration;
using SMURF_Ava.Models;
using SMURF_Ava.Views;

namespace SMURF_Ava.ViewModels;

public class MainWindowViewModel : AbstractEventDrivenViewModel {

    #region CONSTRUCTION

    public MainWindowViewModel(MetaDataObject metaDataObject = null) {
        this._metaDataObject = new MetaDataObject();
        this.ShowTimeStamp = false;
        Init();
    }

    private void Init() {
        this.LogManagerViewModel = LogManager_ViewModel.GetInstance();
        this.PopupManagerViewModel = new PopupManager_ViewModel();
        this.ApplicationList = new ObservableCollection<string>(SystemConfiguration.GetInstance().GetAppList());
        this.CommandItemContainerViewModel = new CommandItemContainer_ViewModel();
        this.TcpReceivedItemViewModels = new ObservableCollection<ResponseItem_ViewModel>();
        
        this.StudiesStringItemManagerViewModel = new StringItemManager_ViewModel();
        this.StudiesStringItemManagerViewModel.RegisterObserver(this);
        
        this.SeriesStringItemManagerViewModel = new StringItemManager_ViewModel();
        this.SeriesStringItemManagerViewModel.RegisterObserver(this);
        
        this.CommandItemContainerViewModel.RegisterObserver(this);
        
        this.SelectedAppName = ApplicationList[0];

        InitLanguageList();

        MetaDataObject metaDataObject = DataStorageManager_ViewModel.GetInstance().ReadFromFile();
        if (metaDataObject != null) LoadMetaData(metaDataObject);
    }

    private void InitLanguageList() {
        this.LanguageOptionList = new ObservableCollection<string>();
        List<string> list = SystemConfiguration.GetInstance().GetLangNameList();
        for (int i = 0; i < list.Count; i++) {
            this.LanguageOptionList.Add(list[i]);
        }

        this.SelectedLanguage = this.LanguageOptionList[0];
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

    public ObservableCollection<string> LanguageOptionList { get; private set; }

    public ObservableCollection<ResponseItem_ViewModel> TcpReceivedItemViewModels { get; private set; }

    public StringItemManager_ViewModel StudiesStringItemManagerViewModel { get; private set; }

    public StringItemManager_ViewModel SeriesStringItemManagerViewModel { get; private set; }


    private string _userName;

    public string UserName {
        get {
            return _userName;
        }
        set {
            if(_userName == value)return;
            _userName = value;
            this._metaDataObject.user = value;
            this.Saved = false;
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
            this.Saved = false;
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
            this.Saved = false;
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

    private string _selectedLanguage;

    public string SelectedLanguage {
        get {
            return _selectedLanguage;
        }
        set {
            if(_selectedLanguage == value)return;
            _selectedLanguage = value;
            this._metaDataObject.language = SystemConfiguration.GetInstance().GetLanguageValueByName(value);
            this.Saved = false;
            RisePropertyChanged(nameof(SelectedLanguage));
        }
    }

    /*private string _studyUid;

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


    private string _seriesUid;

    public string SeriesUid {
        get {
            return _seriesUid;
        }
        set {
            if (_seriesUid == value) return;
            _seriesUid = value;
            this._metaDataObject.selectedSeries = value;
            this.Saved = false;
            RisePropertyChanged(nameof(SeriesUid));
        }
    }*/

    private bool _showTimeStamp;

    public bool ShowTimeStamp {
        get {
            return _showTimeStamp;
        }
        set {
            if(_showTimeStamp == value)return;
            _showTimeStamp = value;
            this._metaDataObject.showTimeStamp = value.ToString();
            this.Saved = false;
            RisePropertyChanged(nameof(ShowTimeStamp));
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

    private ResponseItem_ViewModel _selectedResponse;

    public ResponseItem_ViewModel SelectedResponse {
        get {
            return _selectedResponse;
        }
        set {
            if(_selectedResponse == value)return;
            _selectedResponse = value;
            RisePropertyChanged(nameof(SelectedResponse));
        }
    }


    #endregion

    #region METHODS

    private void LoadMetaData(MetaDataObject metaDataObject) {
        this._metaDataObject = metaDataObject;
        this.ClientPath = metaDataObject.clientPath;
        this.UserName = metaDataObject.user;
        this.Password = metaDataObject.password;
        this.DomainUrl = metaDataObject.serverDomain;
        this.SelectedLanguage = SystemConfiguration.GetInstance().GetLanguageNameByValue(metaDataObject.language);
        //this.StudyUid = metaDataObject.selectedStudy;
        this.StudiesStringItemManagerViewModel.LoadStringItemCollectionFromJson(metaDataObject.StudyStringItemsJson);
        this.SeriesStringItemManagerViewModel.LoadStringItemCollectionFromJson(metaDataObject.SeriesStringItemsJson); 
        //this.SeriesUid = metaDataObject.selectedSeries;
        this.SelectedAppName = metaDataObject.application;
        this.ShowTimeStamp = Convert.ToBoolean(metaDataObject.showTimeStamp);
        this.Saved = true;
    }

    #endregion

    #region COMMANDS

    public void ShowTimeStampCommand() {
        this.ShowTimeStamp = !ShowTimeStamp;
    }

    public void SaveCommand(object o = null) {
        this.Saved = true;
        this._metaDataObject.StudyStringItemsJson = JsonConvert.SerializeObject(this.StudiesStringItemManagerViewModel.StringItemViewModels);
        this._metaDataObject.SeriesStringItemsJson = JsonConvert.SerializeObject(this.SeriesStringItemManagerViewModel.StringItemViewModels);
        DataStorageManager_ViewModel dataStorageManagerViewModel = DataStorageManager_ViewModel.GetInstance();
        dataStorageManagerViewModel.SaveToFile(this._metaDataObject);
    }

    public void CloseClientCommand(object o = null) {
        SystemFacade.GetInstance().InvokeRpcCommand("exit", _metaDataObject);
    }

    public void ClearLogCommand() {
        SystemFacade.GetInstance().ClearLogCommand();
    }

    public void OpenInCmdCommand() {
        string cmdString = "/k cd " + this.ClientPath;
        ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd.exe", cmdString);
        processStartInfo.WorkingDirectory = this.ClientPath;
        Process.Start(processStartInfo);
    }

    public void ClearAllRespondsCommand() {
        SystemFacade.GetInstance().ClearAllRespondsCommand();
    }

    public void ManageStudyCommand() {
        this.PopupManagerViewModel.PupupStringManagerWindow(this.StudiesStringItemManagerViewModel, "Studies");
    }

    public void OpenDatabaseCommand() {
        
    }

    public void ManageSeriesCommand() {
        this.PopupManagerViewModel.PupupStringManagerWindow(this.SeriesStringItemManagerViewModel, "Series");
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

        if (propertyName.Equals(nameof(SystemFacade.PublishResponseItemManager))) {
            ResponseItemManager responseItemManager = (ResponseItemManager)o;
            responseItemManager.RegisterObserver(this);
            return;
        }

        if (propertyName.Equals(nameof(ResponseItemManager.AddItem))) {
            ResponseItem responseItem = (ResponseItem)o;
            ResponseItem_ViewModel itemViewModel = new ResponseItem_ViewModel(responseItem, this.TcpReceivedItemViewModels.Count);
            this.TcpReceivedItemViewModels.Add(itemViewModel);
            this.SelectedResponse = itemViewModel;
            return;
        }

        if (propertyName.Equals(nameof(ResponseItemManager.RemoveAllItems))) {
            this.SelectedResponse = null;
            this.TcpReceivedItemViewModels = new ObservableCollection<ResponseItem_ViewModel>();
            RisePropertyChanged(nameof(TcpReceivedItemViewModels));
            return;
        }

        if (propertyName.Equals(nameof(CommandItem_ViewModel.InvokeRpcCommand))) {
            string commandName = (string)o;
            SystemFacade.GetInstance().InvokeRpcCommand(commandName, _metaDataObject);
            return;
        }

        if (propertyName.Equals(nameof(StringItemManager_ViewModel.PublishSaveEvent))) {
            this.Saved = false;
            return;
        }

        if (propertyName.Equals(nameof(StringItemManager_ViewModel.ContentString))) {
            StringItemManager_ViewModel managerViewModel = (StringItemManager_ViewModel)o;
            this.Saved = false;
            if (managerViewModel == StudiesStringItemManagerViewModel) {
                _metaDataObject.selectedStudy = managerViewModel.ContentString;
                return;
            }

            if (managerViewModel == SeriesStringItemManagerViewModel) {
                _metaDataObject.selectedSeries = managerViewModel.ContentString;
                return;
            }
        }
    }
}