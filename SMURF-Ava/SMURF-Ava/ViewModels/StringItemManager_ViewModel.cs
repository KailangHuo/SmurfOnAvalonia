using System.Collections.ObjectModel;
using DynamicData;
using EventDrivenElements;
using Newtonsoft.Json;

namespace SMURF_Ava.Models;

public class StringItemManager_ViewModel : AbstractEventDrivenViewModel{

    public StringItemManager_ViewModel() {
        this.StringItemViewModels = new ObservableCollection<StringItem_ViewModel>();
        StringItem_ViewModel stringItemViewModel = new StringItem_ViewModel();
        stringItemViewModel.RegisterObserver(this);
        this.StringItemViewModels.Add(stringItemViewModel);
        UpdateCount();
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
            PublishEvent(nameof(ContentString), this);
            RisePropertyChanged(nameof(ContentString));
        }
    }

    private int _count;

    public int Count {
        get {
            return _count;
        }
        set {
            if(_count == value)return;
            _count = value;
            RisePropertyChanged(nameof(Count));
        }
    }

    public ObservableCollection<StringItem_ViewModel> StringItemViewModels { get; private set; }

    public void AddItem(StringItem_ViewModel stringItemViewModel) {
        this.StringItemViewModels.Add(stringItemViewModel);
        stringItemViewModel.RegisterObserver(this);
        UpdateCount();
        PublishSaveEvent();
    }

    public void RemoveItem(StringItem_ViewModel stringItemViewModel) {
        if(this.StringItemViewModels.Count == 1) return;
        if(!this.StringItemViewModels.Contains(stringItemViewModel)) return;
        this.StringItemViewModels.Remove(stringItemViewModel);
        stringItemViewModel.DeregisterObserver(this);
        UpdateCount();
        PublishSaveEvent();
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
        PublishEvent(nameof(ContentString), this);
        RisePropertyChanged(nameof(ContentString));
    }

    private void UpdateCount() {
        this.Count = this.StringItemViewModels.Count;
    }

    private void LoadByContent() {
        
        this.StringItemViewModels = new ObservableCollection<StringItem_ViewModel>();
        if (string.IsNullOrEmpty(_contentString)) {
            StringItem_ViewModel stringItemViewModel = new StringItem_ViewModel();
            stringItemViewModel.RegisterObserver(this);
            this.StringItemViewModels.Add(stringItemViewModel);
            UpdateCount();
            return;
        }

        string[] contentStrs = ContentString.Split(",");
        for (int i = 0; i < contentStrs.Length; i++) {
            StringItem_ViewModel stringItemViewModel = new StringItem_ViewModel(contentStrs[i]);
            stringItemViewModel.RegisterObserver(this);
            this.StringItemViewModels.Add(stringItemViewModel);
        }
        UpdateCount();
        
    }

    public void CopyCollection(StringItemManager_ViewModel stringItemManagerViewModel) {
        this.StringItemViewModels = new ObservableCollection<StringItem_ViewModel>();
        for (int i = 0; i < stringItemManagerViewModel.StringItemViewModels.Count; i++) {
            StringItem_ViewModel stringItemViewModel = stringItemManagerViewModel.StringItemViewModels[i].GetDeepCopy();
            stringItemViewModel.RegisterObserver(this);
            this.StringItemViewModels.Add(stringItemViewModel);
        }
        UpdateCount();
    }

    public void SetStringItemCollection(ObservableCollection<StringItem_ViewModel> collection) {
        this.StringItemViewModels = collection;
        ReformContent();
        UpdateCount();
        RisePropertyChanged(nameof(StringItemViewModels));
        PublishSaveEvent();
    }

    public void LoadStringItemCollectionFromJson(string jsonStr) {
        ObservableCollection<StringItem_ViewModel> stringItemViewModels = 
            JsonConvert.DeserializeObject<ObservableCollection<StringItem_ViewModel>>(jsonStr);
        this.StringItemViewModels = stringItemViewModels;
        ReformContent();
        UpdateCount();
    }

    public void PublishSaveEvent() {
        PublishEvent(nameof(PublishSaveEvent), null);
    }

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(StringItem_ViewModel.RemoveThisCommand))) {
            StringItem_ViewModel stringItemViewModel = (StringItem_ViewModel)o;
            this.RemoveItem(stringItemViewModel);
            return;
        }

        if (propertyName.Equals(nameof(StringItem_ViewModel.IsMuted))) {
            PublishSaveEvent();
            return;
        }
    }
}