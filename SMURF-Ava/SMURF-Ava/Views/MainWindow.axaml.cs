using System;
using Avalonia.Controls;

namespace SMURF_Ava.Views;

public partial class MainWindow : Window {
    public MainWindow() {
        InitializeComponent();
    }
        

    private void SystemInfo_OnTextChanged(object sender, TextChangedEventArgs e) {
        SystemInfoScrollViewer.ScrollToEnd();
    }

    private void SystemInfo_ScrollViewer_OnDataContextChanged(object? sender, EventArgs e) {
        ScrollViewer sc = (ScrollViewer)sender;
        sc.ScrollToEnd();
    }

    /*private void StyledElement_OnDataContextChanged(object? sender, EventArgs e) {
        SystemInfoScrollViewer.ScrollToEnd();
    }*/
}