using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Input.Platform;
using EventDrivenElements;
using SMURF_Ava.Models;

namespace SMURF_Ava.ViewModels;

public class LogItem_ViewModel : AbstractEventDrivenViewModel {

    public LogItem_ViewModel(LogItem logItem):base(logItem) {
        this._logItem = logItem;
        this.ForeGroundColorStr = logItem.LogType == LogTypeEnum.RESPOND ? "Yellow" :
            logItem.LogType == LogTypeEnum.SYSTEM_OPERATION ? "White" : "DarkGray";
        this.CopyAble = false;
        this.TimeStamp = logItem.TimeStamp.ToString();
        string prefix = logItem.LogType == LogTypeEnum.SYSTEM_OPERATION ? ">>> " :
            logItem.LogType == LogTypeEnum.SYSTEM_OPERATION ? "  =>  " : "[SYSTEM]: ";
        this.Content = prefix + logItem.Content;
        this.ContentWithTimeStamp = logItem.TimeStamp.ToString() + " " + logItem.Content;
    }

    private LogItem _logItem;

    private string _foreGroundColorStr;

    public string ForeGroundColorStr {
        get {
            return _foreGroundColorStr;
        }
        set {
            if (_foreGroundColorStr == value) return;
            _foreGroundColorStr = value;
            RisePropertyChanged(nameof(ForeGroundColorStr));
        }
    }

    private string _timeStamp;

    public string TimeStamp {
        get {
            return _timeStamp;
        }
        set {
            if(_timeStamp == value)return;
            _timeStamp = value;
            RisePropertyChanged(nameof(TimeStamp));
        }
    }
    
    private string _content;

    public string Content {
        get {
            return _content;
        }
        set {
            if(_content == value)return;
            _content = value;
            RisePropertyChanged(nameof(Content));
        }
    }

    private string _contentWithTimeStamp;

    public string ContentWithTimeStamp {
        get {
            return _contentWithTimeStamp;
        }
        set {
            if(_contentWithTimeStamp == value)return;
            _contentWithTimeStamp = value;
            RisePropertyChanged(ContentWithTimeStamp);
        }
    }

    private bool _copyAble;

    public bool CopyAble {
        get {
            return _copyAble;
        }
        set {
            if(this._logItem.LogType != LogTypeEnum.SYSTEM_OPERATION) return;
            if(_copyAble == value)return;
            _copyAble = value;
            RisePropertyChanged(nameof(CopyAble));
        }
    }


    public void CopyCommand() {
        this._logItem.CopyContent();
        
    }

}