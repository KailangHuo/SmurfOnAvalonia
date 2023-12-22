using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using AvaloniaApplication1.Models;
using DynamicData;
using ReactiveUI;

namespace AvaloniaApplication1.ViewModels; 

public class MusicStoreViewModel : ViewModelBase {

    public MusicStoreViewModel() {
        this.BuyMusicCommand = ReactiveCommand.Create(() => {
            return SelectedAlbum;
        });
        
        this.SearchResult = new ObservableCollection<AlbumViewModel>();
        this.WhenAnyValue(x => x.SearchText)
            .Throttle(TimeSpan.FromMilliseconds(400))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(DoSearch!);
    }

    private async void DoSearch(string s) {
        IsBusy = true;
        SearchResult.Clear();

        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = _cancellationTokenSource.Token;
        
        if (!string.IsNullOrWhiteSpace(s)) {
            var albums = await Album.SearchAsync(s);

            foreach (var albunm in albums) {
                var vm = new AlbumViewModel(albunm);
                SearchResult.Add(vm);
            }
            
            if(!cancellationToken.IsCancellationRequested) LoadCovers(cancellationToken);
            
        }

        IsBusy = false;
    }

    private AlbumViewModel _selectedAlbum;

    public AlbumViewModel SelectedAlbum {
        get {
            return _selectedAlbum;
        }
        set {
            if(_selectedAlbum == value)return;
            _selectedAlbum = value;
            this.RaiseAndSetIfChanged(ref _selectedAlbum, value);
        }
    }
    

    public ObservableCollection<AlbumViewModel> SearchResult { get; private set; }

    private string? _searchText;

    public string? SearchText {
        get {
            return _searchText;
        }
        set {
            this.RaiseAndSetIfChanged(ref _searchText, value);
        }
    }
    
    
    private bool _isBusy;

    public bool IsBusy {
        get {
            return _isBusy;
        }
        set {
            this.RaiseAndSetIfChanged(ref _isBusy, value);
        }
    }

    private CancellationTokenSource? _cancellationTokenSource;

    private async void LoadCovers(CancellationToken cancellationToken) {
        foreach (var album in SearchResult.ToList()) {
            await album.LoadCover();
            
            if(cancellationToken.IsCancellationRequested) return;
        }
    }

    public ReactiveCommand<Unit, AlbumViewModel?> BuyMusicCommand { get; private set; }

}