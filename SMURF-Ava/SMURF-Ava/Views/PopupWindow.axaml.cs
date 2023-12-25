using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SMURF_Ava.Views;

public partial class PopupWindow : Window {
    public PopupWindow(string msg = null) {
        InitializeComponent();
        if (string.IsNullOrEmpty(msg)) {
            msg = "processing....";
        }

        MessageText.Text = msg;
    }
}