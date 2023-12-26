using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace SMURF_Ava.Views;

public partial class PopupWindow : Window {
    public PopupWindow(string content) {
        InitializeComponent();
        ExceptionTextBlock.Text = content;
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e) {
        this.Close();
    }
}