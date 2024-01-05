using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Input.Platform;
using Avalonia.Media.Imaging;
using EventDrivenElements;
using SMURF_Ava.Models;

namespace SMURF_Ava.ViewModels;

public class ResponseItem_ViewModel : AbstractEventDrivenViewModel{

    public ResponseItem_ViewModel(ResponseItem responseItem, int indexNum):base(responseItem) {
        this.IndexNumber = indexNum;
        this._responseItem = responseItem;
        init();
    }

    private void init() {
        this.TimeStamp = _responseItem.TimeStamp.ToString();
        this.Guid = _responseItem.Guid;
        this.StatusType = _responseItem.StatusType;
        this.StatusParam = _responseItem.ResponseItemStatusParamObject.ToString();
        this.Content = _responseItem.RawContent;
        this.ImageView = Base64ToImage(_responseItem.ResponseItemStatusParamObject.ImageList[0]);
    }

    private ResponseItem _responseItem;

    #region NOTIFIABLE_PROPERTIES

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

    private string _guid;

    public string Guid {
        get {
            return _guid;
        }
        set {
            if(_guid == value)return;
            _guid = value;
            RisePropertyChanged(nameof(Guid));
        }
    }
    
    private string _statusType;

    public string StatusType {
        get {
            return _statusType;
        }
        set {
            if(_statusType == value)return;
            _statusType = value;
            RisePropertyChanged(nameof(StatusType));
        }
    }
    
    private string _statusParam;

    public string StatusParam {
        get {
            return _statusParam;
        }
        set {
            if(_statusParam == value)return;
            _statusParam = value;
            RisePropertyChanged(nameof(StatusParam));
        }
    }

    private Bitmap _imageView;

    public Bitmap ImageView {
        get {
            return _imageView;
        }
        set {
            if(_imageView == value)return;
            _imageView = value;
            RisePropertyChanged(nameof(ImageView));
        }
    }

    #endregion
    
    public void CopyContentCommand() {
        IClipboard clipboard = SystemFacade.GetInstance().MainWindow.Clipboard;
        clipboard.SetTextAsync(this._content); 
    }

    private Bitmap Base64ToImage(string base64Str) {
        byte[] imageBytes = Convert.FromBase64String(base64Str);
        MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
        ms.Write(imageBytes, 0, imageBytes.Length);
        Bitmap bitmap = new Bitmap(ms);
        return bitmap;
    }

}