using System.Threading.Tasks;
using Avalonia.Controls.Chrome;
using Avalonia.Media.Imaging;
using AvaloniaApplication1.Models;
using ReactiveUI;

namespace AvaloniaApplication1.ViewModels; 

public class AlbumViewModel : ViewModelBase {

    private readonly Album _album;

    public AlbumViewModel(Album album) {
        _album = album;
    }

    public string Artist => _album.Artist;

    public string Title => _album.Title;

    private Bitmap? _cover;

    public Bitmap? Cover
    {
        get => _cover;
        private set => this.RaiseAndSetIfChanged(ref _cover, value);
        /*get {
            return _cover;
        }
        set {
            _cover = value;
            this.RaiseAndSetIfChanged(ref _cover, value);
        }*/
    }

    public async Task LoadCover() {
        await using (var imageStream = await _album.LoadCoverBitmapAsync()) {
            Cover = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));
        }
    }

}