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
    }

    private string _contentString;

    public string ContentString {
        get {
            return _contentString;
        }
        set {
            if(_contentString == value)return;
            _contentString = value;
            LoadByContent();
            RisePropertyChanged(nameof(ContentString));
        }
    }

    private bool _canModifyContent;

    public bool CanModifyContent {
        get {
            return _canModifyContent;
        }
        set {
            if(_canModifyContent == value)return;
            _canModifyContent = value;
            RisePropertyChanged(nameof(CanModifyContent));
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

    public void ReformContent() {
        string s = "";
        bool hasFirst = false;
        for (int i = 0; i < this.StringItemViewModels.Count; i++) {
            if(this.StringItemViewModels[i].IsMuted) continue;
            if (hasFirst) s += ",";
            s += this.StringItemViewModels[i].Content;
            hasFirst = true;
        }

        this._contentString = s;
        RisePropertyChanged(nameof(ContentString));
    }

    public void LoadContent(string str) {
        
    }

    private void LoadByContent() {
        this.StringItemViewModels = new ObservableCollection<StringItem_ViewModel>();
        if (string.IsNullOrEmpty(_contentString)) {
            StringItem_ViewModel stringItemViewModel = new StringItem_ViewModel();
            stringItemViewModel.RegisterObserver(this);
            this.StringItemViewModels.Add(stringItemViewModel);
            return;
        }

        string[] contentStrs = ContentString.Split(",");
        for (int i = 0; i < contentStrs.Length; i++) {
            StringItem_ViewModel stringItemViewModel = new StringItem_ViewModel(contentStrs[i]);
            stringItemViewModel.RegisterObserver(this);
            this.StringItemViewModels.Add(stringItemViewModel);
        }
    }

    public void CopyCollection(StringItemManager_ViewModel stringItemManagerViewModel) {
        this.StringItemViewModels = new ObservableCollection<StringItem_ViewModel>();
        for (int i = 0; i < stringItemManagerViewModel.StringItemViewModels.Count; i++) {
            StringItem_ViewModel stringItemViewModel = stringItemManagerViewModel.StringItemViewModels[i].GetDeepCopy();
            stringItemViewModel.RegisterObserver(this);
            this.StringItemViewModels.Add(stringItemViewModel);
        }
    }

    public void SetStringItemCollection(ObservableCollection<StringItem_ViewModel> collection) {
        this.StringItemViewModels = collection;
        ReformContent();
        RisePropertyChanged(nameof(StringItemViewModels));
    }

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(StringItem_ViewModel.RemoveThisCommand))) {
            StringItem_ViewModel stringItemViewModel = (StringItem_ViewModel)o;
            this.RemoveItem(stringItemViewModel);
            return;
        }
    }
}