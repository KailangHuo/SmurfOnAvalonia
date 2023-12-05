using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AvaloniaApplication1.ViewModels;
using iTunesSearch.Library;

namespace AvaloniaApplication1.Models; 

public class Album {
    
    private static iTunesSearchManager s_SearchManager = new iTunesSearchManager();

    private static HttpClient s_httpClient = new HttpClient();
    
    private string CachePath => $"s."

    public string Artist { get; set; }

    public string Title { get; set; }

    public string CoverUrl { get; set; }

    public Album(string artist, string title, string coverUrl) {
        this.Artist = artist;
        this.Title = title;
        this.CoverUrl = coverUrl;
    }

    public static async Task<IEnumerable<Album>> SearchAsync(string searchTerm) {
        var query = await s_SearchManager.GetAlbumsAsync(searchTerm).ConfigureAwait(false);

        return query.Albums.Select(x => new Album(x.ArtistName,
            x.CollectionName,
            x.ArtworkUrl100.Replace("100x100bb", "600x600bb")));
    }

}