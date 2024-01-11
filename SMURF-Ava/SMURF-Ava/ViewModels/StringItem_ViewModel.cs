using EventDrivenElements;

namespace SMURF_Ava.Models;

public class StringItem_ViewModel : AbstractEventDrivenViewModel {

    public StringItem_ViewModel(string content = null) {
        if (!string.IsNullOrEmpty(content)) Content = content;
    }


    private bool _isMuted;

    public bool IsMuted {
        get {
            return _isMuted;
        }
        set {
            if(_isMuted == value)return;
            _isMuted = value;
            PublishEvent(nameof(IsMuted), this);
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

    public StringItem_ViewModel GetDeepCopy() {
        StringItem_ViewModel stringItemViewModel = new StringItem_ViewModel();
        stringItemViewModel.IsMuted = this.IsMuted;
        stringItemViewModel.Content = this.Content;
        return stringItemViewModel;
    }

    public void RemoveThisCommand() {
        PublishEvent(nameof(RemoveThisCommand), this);
    }

}