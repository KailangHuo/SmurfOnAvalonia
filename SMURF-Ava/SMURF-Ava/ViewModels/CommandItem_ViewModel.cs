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
    
    private bool _isSelected;

    public bool IsSelected {
        get {
            return _isSelected;
        }
        set {
            if(_isSelected ==value)return;
            _isSelected = value;
            RisePropertyChanged(nameof(IsSelected));
        }
    }
    
    public void InvokeRpcCommand() {
        PublishEvent(nameof(InvokeRpcCommand), CommandName);
    }

}