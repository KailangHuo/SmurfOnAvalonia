using EventDrivenElements;

namespace SMURF_Ava.ViewModels;

public class CommandItem_ViewModel : AbstractEventDrivenViewModel {

    public CommandItem_ViewModel(string name, int indexNum) {
        this.CommandName = name;
        this.IsSelected = false;
        this.IndexNumber = indexNum;
    }

    private int _indexNumber;

    public int IndexNumber {
        get {
            return _indexNumber;
        }
        set {
            if(_indexNumber == value)return;
            _indexNumber = value;
            RisePropertyChanged(nameof(IndexNumber));
        }
    }

    private string _commandName;

    public string CommandName {
        get {
            return _commandName;
        }
        set {
            if(_commandName ==value)return;
            _commandName = value;
            RisePropertyChanged(nameof(CommandName));
        }
    }

    private bool _isHovered;

    public bool IsHovered {
        get {
            return _isHovered;
        }
        set {
            if(_isHovered == value)return;
            _isHovered = value;
            IsDetailed = _isSelected || _isHovered;
            RisePropertyChanged(nameof(IsHovered));
        }
    }

    private bool _isSelected;

    public bool IsSelected {
        get {
            return _isSelected;
        }
        set {
            if(_isSelected ==value)return;
            _isSelected = value;
            IsDetailed = _isSelected || _isHovered;
            RisePropertyChanged(nameof(IsSelected));
        }
    }

    private bool _isDetailed;

    public bool IsDetailed {
        get {
            return _isDetailed;
        }
        set {
            if(_isDetailed == value)return;
            _isDetailed = value;
            RisePropertyChanged(nameof(IsDetailed));
        }
    }

    public void InvokeRpcCommand() {
        PublishEvent(nameof(InvokeRpcCommand), CommandName);
    }

}