using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using Avalonia.ReactiveUI;
using AvaloniaApplication1.ViewModels;
using ReactiveUI;

namespace AvaloniaApplication1.Views; 

public partial class MusicStoreWindow : ReactiveWindow<MusicStoreViewModel> {
    public MusicStoreWindow() {
        InitializeComponent();
        this.WhenActivated(action => action(ViewModel!.BuyMusicCommand.Subscribe(Close)));
    }
}