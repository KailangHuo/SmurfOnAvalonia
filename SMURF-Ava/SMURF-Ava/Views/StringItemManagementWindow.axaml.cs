using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DynamicData;
using SMURF_Ava.Models;

namespace SMURF_Ava.Views;

public partial class StringItemManagementWindow : Window , INotifyPropertyChanged{
    
    public StringItemManagementWindow(StringItemManager_ViewModel stringItemManagerViewModel, string titleName) {
        this.TitleStr = titleName;
        this.originalStringItemManagerViewModel = stringItemManagerViewModel;
        this.StringItemManagerViewModel = new StringItemManager_ViewModel();
        this.StringItemManagerViewModel.CopyCollection(stringItemManagerViewModel);
        this.DataContext =  this.StringItemManagerViewModel;
        InitializeComponent();
    }

    private StringItemManager_ViewModel originalStringItemManagerViewModel;

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
    
    public StringItemManager_ViewModel StringItemManagerViewModel { get; private set; }

    public int CollectionCount {
        get {
            return this.StringItemManagerViewModel.StringItemViewModels.Count;
        }
    }

    public void ConfirmCommand() {
        for (int i = this.StringItemManagerViewModel.StringItemViewModels.Count - 1; i >= 0; i--) {
            StringItem_ViewModel stringItem = this.StringItemManagerViewModel.StringItemViewModels[i];
            if (string.IsNullOrEmpty(stringItem.Content)) {
                this.StringItemManagerViewModel.RemoveItem(stringItem);
            }
        }
        this.StringItemManagerViewModel.ReformContent();
        this.originalStringItemManagerViewModel.SetStringItemCollection(this.StringItemManagerViewModel.StringItemViewModels);
        this.Close();
    }

    public void CancelCommand() {
        this.Close();
    }

    public void AddItemCommand() {
        this.StringItemManagerViewModel.AddItem(new StringItem_ViewModel());
        RisePropertyChanged(nameof(CollectionCount));
    }
    
    private void ItemsControl_OnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e) {
        if (e.Property.Name == "ItemCount") {
            RisePropertyChanged(nameof(CollectionCount));
        }
    }

    #region IPROPERTY_CHANGED

    public event PropertyChangedEventHandler? PropertyChanged;

    public void RisePropertyChanged(string propertyName)
    {
        if (this.PropertyChanged == null)
            return;
        this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
    
}