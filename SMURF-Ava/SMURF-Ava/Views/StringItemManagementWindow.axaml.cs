using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SMURF_Ava.Models;

namespace SMURF_Ava.Views;

public partial class StringItemManagementWindow : Window , INotifyPropertyChanged{
    
    public StringItemManagementWindow() {
        this.TitleStr = "Management Window";
        InitializeComponent();
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

    public void CloseThisCommand() {
        this.Close();
    }

    public void AddItemCommand() {
        StringItemManager_ViewModel stringItemManagerViewModel = (StringItemManager_ViewModel)this.DataContext;
        stringItemManagerViewModel.AddItem(new StringItem_ViewModel());
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void RisePropertyChanged(string propertyName)
    {
        if (this.PropertyChanged == null)
            return;
        this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

}