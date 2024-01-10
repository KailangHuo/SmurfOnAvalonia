using EventDrivenElements;

namespace SMURF_Ava.Models;

public class StringItem_ViewModel : AbstractEventDrivenViewModel {

    public StringItem_ViewModel() {
        
    }


    private bool _isMuted;

    public bool IsMuted {
        get {
            return _isMuted;
        }
        set {
            if(_isMuted == value)return;
            _isMuted = value;
            RisePropertyChanged(nameof(IsMuted));
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

    public void RemoveThisCommand() {
        PublishEvent(nameof(RemoveThisCommand), this);
    }

}