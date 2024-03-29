﻿using System.Collections.Generic;
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
            CommandItem_ViewModel commandItemViewModel = new CommandItem_ViewModel(cmdNameList[i], i);
            commandItemViewModel.RegisterObserver(this);
            this.CommandItemViewModels.Add(commandItemViewModel);
        }

        this.SelectedItem = CommandItemViewModels[0];
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
        if (propertyName.Equals(nameof(CommandItem_ViewModel.InvokeRpcCommand))) {
            PublishEvent(nameof(CommandItem_ViewModel.InvokeRpcCommand), o);
            return;
        }

        if (propertyName.Equals(nameof(CommandItem_ViewModel.IsSelected))) {
            CommandItem_ViewModel commandItemViewModel = (CommandItem_ViewModel)o;
            this.SelectedItem = commandItemViewModel;
            return;
        }
    }
}