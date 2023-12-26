using EventDrivenElements;

namespace SMURF_Ava.ViewModels;

public class CommandItem_ViewModel : AbstractEventDrivenViewModel {

    public CommandItem_ViewModel(string name) {
        this.CommandName = name;
        this.IsSelected = false;
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
    
    public void InvokeCommand() {
        PublishEvent(nameof(InvokeCommand), CommandName);
    }

}