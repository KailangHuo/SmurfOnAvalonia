using System;
using System.Collections.Generic;
using EventDrivenElements;

namespace SMURF_Ava.Models;

public class SystemLogger : AbstractEventDrivenObject {

    public SystemLogger() {
        this._logItems = new List<LogItem>();
    }
    
    private List<LogItem> _logItems;

    public void Log(string logContent, LogTypeEnum logType) {
        DateTime timeStamp = DateTime.Now;
        LogItem logItem = new LogItem(logType, timeStamp, logContent);
        this._logItems.Add(logItem);
        PublishEvent(nameof(Log), logItem);
    }

}