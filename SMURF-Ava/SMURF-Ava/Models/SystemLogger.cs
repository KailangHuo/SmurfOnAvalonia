﻿using System;
using System.Collections.Generic;
using EventDrivenElements;

namespace SMURF_Ava.Models;

public class SystemLogger : AbstractEventDrivenObject {

    public SystemLogger() {
        this._logItems = new List<LogItem>();
    }
    
    private List<LogItem> _logItems;

    public void ClearAllLog() {
        _logItems = new List<LogItem>();
        PublishEvent(nameof(ClearAllLog), null);
        UpdateLog("Log cleared.", LogTypeEnum.SYSTEM_NOTIFICATION);
    }

    public void UpdateLog(string logContent, LogTypeEnum logType, string copyContent = "") {
        DateTime timeStamp = DateTime.Now;
        LogItem logItem = new LogItem(logType, timeStamp, logContent, copyContent);
        this._logItems.Add(logItem);
        PublishEvent(nameof(UpdateLog), logItem);
    }

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(CommandLineCommunicator.PublishCommandStart))) {
            UIH_Command uihCommand = (UIH_Command)o;
            string logContent = uihCommand.CommandBody;
            string copyContent = uihCommand.CommandHeader + " " + uihCommand.CommandBody;
            UpdateLog(logContent, LogTypeEnum.SYSTEM_OPERATION, copyContent);
            return;
        }

        if (propertyName.Equals(nameof(CommandLineCommunicator.PublishRespondReceived))) {
            string resultStr = (string)o;
            UpdateLog(resultStr, LogTypeEnum.RESPOND);
            return;
        }
    }
}