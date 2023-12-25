using System;

namespace SMURF_Ava.Models;

public class LogItem {

    public LogItem(LogTypeEnum logTypeEnum, DateTime timeStamp, string content) {
        this.LogType = logTypeEnum;
        this.TimeStamp = TimeStamp;
        this.Content = content;
    }

    public LogTypeEnum LogType;

    public DateTime TimeStamp;

    public string Content;

}

public enum LogTypeEnum {
    SYSTEM_NOTIFICATION,
    SYSTEM_OPERATION,
    RESPOND
}