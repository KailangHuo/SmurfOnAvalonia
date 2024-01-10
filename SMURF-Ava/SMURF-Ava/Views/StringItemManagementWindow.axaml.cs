using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DynamicData;
using SMURF_Ava.Models;

namespace SMURF_Ava.Views;

public partial class StringItemManagementWindow : Window , INotifyPropertyChanged{
    
    public StringItemManagementWindow(StringItemManager_ViewModel stringItemManagerViewModel) {
        this.TitleStr = "Management Window";
        this.TempStringItemCollection = new ObservableCollection<StringItem_ViewModel>();
        this.DataContext = this;
        Init();
        InitializeComponent();
        
    }

    private void Init() {
        StringItemManager_ViewModel stringItemManagerViewModel = (StringItemManager_ViewModel)DataContext;
        for (int i = 0; i < stringItemManagerViewModel.StringItemViewModels.Count; i++) {
            this.TempStringItemCollection.Add( stringItemManagerViewModel.StringItemViewModels[i]);
        }
    }

    private string _titleStr;

    public string TitleStr {
        get {
            return _titleStr;
        }
        set {
            if(_titleStr == value)return;
            _titleStr = value;
            RisePropertyChanged(nameof(TitleStr));
        }
    }

    public ObservableCollection<StringItem_ViewModel> TempStringItemCollection { get;private set; }

    public int CollectionCount {
        get {
            StringItemManager_ViewModel stringItemManagerViewModel = (StringItemManager_ViewModel)DataContext;
            return stringItemManagerViewModel.StringItemViewModels.Count;
        }
    }
    

    public void CloseThisCommand() {
        this.Close();
    }

    public void AddItemCommand() {
        StringItemManager_ViewModel stringItemManagerViewModel = (StringItemManager_ViewModel)this.DataContext;
        stringItemManagerViewModel.AddItem(new StringItem_ViewModel());
    }
    
    private void ItemsControl_OnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e) {
        if (e.Property.Name == "ItemCount") {
            RisePropertyChanged(nameof(CollectionCount));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void RisePropertyChanged(string propertyName)
    {
        if (this.PropertyChanged == null)
            return;
        this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    
}