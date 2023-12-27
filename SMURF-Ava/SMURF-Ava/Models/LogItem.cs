using System;
using Avalonia.Input.Platform;

namespace SMURF_Ava.Models;

public class LogItem {

    public LogItem(LogTypeEnum logTypeEnum, DateTime timeStamp, string content, string copyContent) {
        this.LogType = logTypeEnum;
        this.TimeStamp = timeStamp;
        this.Content = content;
        this._copyContent = copyContent;
    }

    private string _copyContent;

    public LogTypeEnum LogType;

    public DateTime TimeStamp;

    public string Content;

    public void CopyContent() {
        IClipboard clipboard = SystemFacade.GetInstance().MainWindow.Clipboard;
        clipboard.SetTextAsync(this._copyContent);
    }

}

public enum LogTypeEnum {
    SYSTEM_NOTIFICATION,
    SYSTEM_OPERATION,
    RESPOND
}