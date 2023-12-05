using Avalonia.Controls.Chrome;
using AvaloniaApplication1.Models;

namespace AvaloniaApplication1.ViewModels; 

public class AlbumViewModel : ViewModelBase {

    private readonly Album _album;

    public AlbumViewModel(Album album) {
        _album = album;
    }

    public string Artist => _album.Artist;

    public string Title => _album.Title;

}