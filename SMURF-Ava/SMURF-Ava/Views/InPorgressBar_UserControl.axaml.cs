﻿using System;
using System.ComponentModel;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using SMURF_Ava.ViewModels;

namespace SMURF_Ava.Views;

public partial class InPorgressBar_UserControl : UserControl , INotifyPropertyChanged{
    public InPorgressBar_UserControl() {
        NotificationText = "Processing...";
        InitializeComponent();
    }
    
    public static readonly StyledProperty<string> NotificationTextProperty =
        AvaloniaProperty.Register<InPorgressBar_UserControl, string>(nameof(NotificationText));

    private string _notificationText;
    
    public string NotificationText {
        get {
            return _notificationText;
        }
        set {
            if(_notificationText == value)return;
            _notificationText = value;
            RisePropertyChanged(nameof(NotificationText));
        }
    }

    #region COMMANDS
    
    public void CancelCommand () {
        PopupManager_ViewModel popupManagerViewModel = (PopupManager_ViewModel)DataContext;
        popupManagerViewModel.CancelCurrentCommand();
    }

    #endregion
    
    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;

    public void RisePropertyChanged(string propertyName) {
        if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion

}