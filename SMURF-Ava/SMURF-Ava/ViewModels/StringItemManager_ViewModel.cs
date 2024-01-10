using System.Collections.ObjectModel;
using DynamicData;
using EventDrivenElements;

namespace SMURF_Ava.Models;

public class StringItemManager_ViewModel : AbstractEventDrivenViewModel{

    public StringItemManager_ViewModel() {
        this.StringItemViewModels = new ObservableCollection<StringItem_ViewModel>();
        StringItem_ViewModel stringItemViewModel = new StringItem_ViewModel();
        stringItemViewModel.RegisterObserver(this);
        this.StringItemViewModels.Add(stringItemViewModel);
        this.SelectedStringItem = stringItemViewModel;
    }

    private StringItem_ViewModel _selectedStringItem;

    public StringItem_ViewModel SelectedStringItem {
        get {
            return _selectedStringItem;
        }
        set {
            if(_selectedStringItem == value)return;
            _selectedStringItem = value;
            RisePropertyChanged(nameof(SelectedStringItem));
        }
    }

    public ObservableCollection<StringItem_ViewModel> StringItemViewModels { get; private set; }

    public void AddItem(StringItem_ViewModel stringItemViewModel) {
        this.StringItemViewModels.Add(stringItemViewModel);
        stringItemViewModel.RegisterObserver(this);
    }

    public void RemoveItem(StringItem_ViewModel stringItemViewModel) {
        if(this.StringItemViewModels.Count == 1) return;
        if(!this.StringItemViewModels.Contains(stringItemViewModel)) return;
        this.StringItemViewModels.Remove(stringItemViewModel);
        stringItemViewModel.DeregisterObserver(this);
    }

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(StringItem_ViewModel.RemoveThisCommand))) {
            StringItem_ViewModel stringItemViewModel = (StringItem_ViewModel)o;
            this.RemoveItem(stringItemViewModel);
            return;
        }
    }
}