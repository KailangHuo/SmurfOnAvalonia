using System.Collections.ObjectModel;
using ReactiveUI;

namespace AvaloniaApplication1.ViewModels; 

public class MusicStoreViewModel : ViewModelBase {

    public MusicStoreViewModel() {
        this.SearchResult.Add(new AlbumViewModel());
        this.SearchResult.Add(new AlbumViewModel());
        this.SearchResult.Add(new AlbumViewModel());
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

}