using System.Collections.Generic;
using System.Collections.ObjectModel;
using DynamicData;
using EventDrivenElements;
using SMURF_Ava.configuration;

namespace SMURF_Ava.ViewModels;

public class CommandItemContainer_ViewModel : AbstractEventDrivenViewModel{
    
    
    public CommandItemContainer_ViewModel() {
        this.CommandItemViewModels = new ObservableCollection<CommandItem_ViewModel>();
        InitCommands();
    }

    private void InitCommands() {
        List<string> cmdNameList = SystemConfiguration.GetInstance().GetCmdlineCommandNameList();
        for (int i = 0; i < cmdNameList.Count; i++) {
            CommandItem_ViewModel commandItemViewModel = new CommandItem_ViewModel(cmdNameList[i]);
            commandItemViewModel.RegisterObserver(this);
            this.CommandItemViewModels.Add(commandItemViewModel);
        }
    }

    public ObservableCollection<CommandItem_ViewModel> CommandItemViewModels { get; private set; }

    private CommandItem_ViewModel _selectedItem;

    public CommandItem_ViewModel SelectedItem {
        get {
            return _selectedItem;
        }
        set {
            if(_selectedItem == value) return;
            if(_selectedItem != null)_selectedItem.IsSelected = false;
            _selectedItem = value;
            _selectedItem.IsSelected = true;
            RisePropertyChanged(nameof(SelectedItem));
        }
    }

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(CommandItem_ViewModel.InvokeCommand))) {
            PublishEvent(nameof(CommandItem_ViewModel.InvokeCommand), o);
        }
    }
}