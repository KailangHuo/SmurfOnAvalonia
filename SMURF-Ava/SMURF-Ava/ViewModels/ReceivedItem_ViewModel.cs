using Avalonia.Input.Platform;
using EventDrivenElements;

namespace SMURF_Ava.ViewModels;

public class ReceivedItem_ViewModel : AbstractEventDrivenViewModel{

    public ReceivedItem_ViewModel(string contentStr, int indexNum) {
        string[] strs = contentStr.Split("@###@");
        this.TimeStamp = strs[0];
        this.Content = strs[1];
        this.IndexNumber = indexNum;
    }

    private int _indexNumber;

    public int IndexNumber {
        get {
            return _indexNumber;
        }
        set {
            if (_indexNumber == value) return;
            _indexNumber = value;
            RisePropertyChanged(nameof(IndexNumber));
        }
    }

    private string _timeStamp;

    public string TimeStamp {
        get {
            return _timeStamp;
        }
        set {
            if (_timeStamp == value) return;
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

    public void CopyContentCommand() {
        IClipboard clipboard = SystemFacade.GetInstance().MainWindow.Clipboard;
        clipboard.SetTextAsync(this._content); 
    }

}