using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Threading;
using SMURF_Ava.ViewModels;

namespace SMURF_Ava.Views;

public partial class MainWindow : Window , INotifyPropertyChanged{
    public MainWindow() {
        ShowTimeStamp = false;
        TimestampButtonContent = "show timestamp";
        InitializeComponent();
        this.Closing += CloseApplication;
    }

    private string _timestampButtonContent;

    public string TimestampButtonContent {
        get {
            return _timestampButtonContent;
        }
        set {
            if(_timestampButtonContent == value)return;
            _timestampButtonContent = value;
            RisePropertyChanged(nameof(TimestampButtonContent));
        }
    }

    private bool _showTimeStamp;
    public bool ShowTimeStamp {
        get {
            return _showTimeStamp;
        }
        set {
            if(_showTimeStamp == value)return;
            _showTimeStamp = value;
            if (_showTimeStamp) TimestampButtonContent = "disable timestamp";
            else TimestampButtonContent = "show timestamp";
            RisePropertyChanged(nameof(ShowTimeStamp));
        }
    }


    private void SystemLogListBox_OnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e) {
        if (e.Property.Name == "ItemCount" ) {
            SystemInfo_ScrollViewer.ScrollToEnd();
        }
        
    }
    
    private void SystemLogListBox_OnSizeChanged(object? sender, SizeChangedEventArgs e) {
        if(SystemInfo_ScrollViewer != null) SystemInfo_ScrollViewer.ScrollToEnd();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e) {
        this.ShowTimeStamp = !ShowTimeStamp;
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    public void RisePropertyChanged(string propertyName)
    {
        if (this.PropertyChanged == null)
            return;
        this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }


    private void InputElement_OnPointerEntered(object? sender, PointerEventArgs e) {
        Grid element = (Grid)sender;
        LogItem_ViewModel logItemViewModel = (LogItem_ViewModel)element.DataContext;
        logItemViewModel.CopyAble = true;
    }

    private void InputElement_OnPointerExited(object? sender, PointerEventArgs e) {
        Grid element = (Grid)sender;
        LogItem_ViewModel logItemViewModel = (LogItem_ViewModel)element.DataContext;
        logItemViewModel.CopyAble = false;
    }

    private void CloseApplication(object o, WindowClosingEventArgs e) {
        Process.GetCurrentProcess().Kill();
        System.Environment.Exit(0);
    }

    private void CommandPanel_OnPointerEntered(object? sender, PointerEventArgs e) {
        Grid grid = (Grid)sender;
        CommandItem_ViewModel commandItemViewModel = (CommandItem_ViewModel)grid.DataContext;
        commandItemViewModel.IsHovered = true;
    }

    private void CommandPanel_OnPointerExited(object? sender, PointerEventArgs e) {
        Grid grid = (Grid)sender;
        CommandItem_ViewModel commandItemViewModel = (CommandItem_ViewModel)grid.DataContext;
        commandItemViewModel.IsHovered = false;
    }
}