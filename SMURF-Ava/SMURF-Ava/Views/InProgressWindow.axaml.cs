using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SMURF_Ava.Views;

public partial class InProgressWindow : Window {
    public InProgressWindow(string msg = null) {
        InitializeComponent();
        if (string.IsNullOrEmpty(msg)) {
            msg = "processing....";
        }

        MessageText.Text = msg;
    }
}