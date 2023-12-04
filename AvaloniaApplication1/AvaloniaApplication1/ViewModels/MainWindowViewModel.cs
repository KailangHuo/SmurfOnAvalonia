using System.Reactive.Linq;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using AvaloniaApplication1.Views;
using ReactiveUI;

namespace AvaloniaApplication1.ViewModels;

public class MainWindowViewModel : ViewModelBase {

    public MainWindowViewModel() {
        this.ShowDialog = new Interaction<MusicStoreViewModel, AlbumViewModel?>();
        SetupCommand();
    }

    public ICommand BuyMusicCommand { get; private set; }

    private void SetupCommand() {
        this.BuyMusicCommand = ReactiveCommand.CreateFromTask(async ()=> BuyMusic());
    }

    private async void BuyMusic() {
        Window window = new MusicStoreWindow();
        MusicStoreViewModel store = new MusicStoreViewModel();
        window.DataContext = store;
        var result = await ShowDialog.Handle(store);
    }

    public Interaction<MusicStoreViewModel, AlbumViewModel> ShowDialog { get; }
}