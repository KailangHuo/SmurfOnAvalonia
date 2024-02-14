﻿using System;
using System.Collections.Generic;
using EventDrivenElements;

namespace SMURF_Ava.Models;

public class SystemLogger : AbstractEventDrivenObject {

    private static SystemLogger _instance;

    private SystemLogger() {
        this._logItems = new List<LogItem>();
        this._locker = new object();
    }

    public static SystemLogger GetInstance() {
        if (_instance == null) {
            lock (typeof(SystemLogger)) {
                if (_instance == null) {
                    _instance = new SystemLogger();
                }
            }
        }

        return _instance;
    }

    private object _locker;

    private List<LogItem> _logItems;

    public void ClearAllLog() {
        lock (_locker) {
            _logItems = new List<LogItem>();
        }
        PublishEvent(nameof(ClearAllLog), null);
        UpdateLog("Log cleared.", LogTypeEnum.SYSTEM_NOTIFICATION);
    }

    public void UpdateLog(string logContent, LogTypeEnum logType, string copyContent = "") {
        LogItem logItem = null;
        lock (_locker) {
            DateTime timeStamp = DateTime.Now;
            logItem = new LogItem(logType, timeStamp, logContent, copyContent);
            this._logItems.Add(logItem);
        }
        PublishEvent(nameof(UpdateLog), logItem);
    }

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(CommandLineCommunicator.PublishCommandStart))) {
            Uuu_Command uuuCommand = (Uuu_Command)o;
            string logContent = uuuCommand.CommandBody;
            string copyContent = uuuCommand.CommandHeader + " " + uuuCommand.CommandBody;
            UpdateLog(logContent, LogTypeEnum.COMMAND_OPERATION, copyContent);
            return;
        }

        if (propertyName.Equals(nameof(CommandLineCommunicator.PublishRespondReceived))) {
            string resultStr = (string)o;
            UpdateLog(resultStr, LogTypeEnum.RESPOND);
            return;
        }
    }
}