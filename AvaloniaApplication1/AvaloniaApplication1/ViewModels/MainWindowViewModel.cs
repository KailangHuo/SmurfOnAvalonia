using System.Collections.ObjectModel;
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
        this.Albums = new ObservableCollection<AlbumViewModel>();
        SetupCommand();
    }

    public ICommand BuyMusicCommand { get; private set; }

    private void SetupCommand() {
        this.BuyMusicCommand = ReactiveCommand.CreateFromTask(async () => {
            var store = new MusicStoreViewModel();
            var result = await ShowDialog.Handle(store);
            if (result != null) {
                Albums.Add(result);
                await result.SaveToDiskAsync();
            }
        });
    }

    public Interaction<MusicStoreViewModel, AlbumViewModel> ShowDialog { get; }

    public ObservableCollection<AlbumViewModel> Albums { get; }
}