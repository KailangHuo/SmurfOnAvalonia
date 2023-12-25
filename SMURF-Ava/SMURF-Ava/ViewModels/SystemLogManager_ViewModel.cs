﻿using System;
using System.Collections.ObjectModel;
using DynamicData;
using EventDrivenElements;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SMURF_Ava.Models;

namespace SMURF_Ava.ViewModels;

public class SystemLogManager_ViewModel : AbstractEventDrivenViewModel{

    public SystemLogManager_ViewModel() {
        LogItemViewModels = new ObservableCollection<LogItem_ViewModel>();
        LogInfo = "";
        LogInfo += "System Init!";
    }

    public ObservableCollection<LogItem_ViewModel> LogItemViewModels { get; private set; }

    private string logInfo;

    public void AddLogItem(LogItem_ViewModel logItemViewModel) {
        this.LogItemViewModels.Add(logItemViewModel);
    }

    public string LogInfo {
        get {
            return logInfo;
        }
        set {
            if(logInfo == value)return;
            logInfo = value;
            RisePropertyChanged(nameof(LogInfo));
        }
    }

    private void UpdateLog(string logContent) {
        DateTime timeStamp = DateTime.Now;
        string log = "\n" + timeStamp.ToString() + " " + logContent;
        LogInfo += log;
    }

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(SystemLogger.UpdateLog))) {
            LogItem_ViewModel logItemViewModel = new LogItem_ViewModel((LogItem)o);
            AddLogItem(logItemViewModel);
        }
    }
}