using DigitalMusicLibrary.Models;
using Newtonsoft.Json;

namespace DigitalMusicLibrary.Data
{

    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Artists.Any())
            {
                return;  
            }

            var json = File.ReadAllText("C:/Users/Cosmin Moisa/Desktop/data.json");
            var artists = JsonConvert.DeserializeObject<List<Artist>>(json);

            int artistId = 1, albumId = 1, songId = 1;
            try
            {
                foreach (var artist in artists)
                {
                    artist.Id = artistId++;
                    foreach (var album in artist.Albums)
                    {
                        album.ArtistId = artist.Id;
                        album.Id = albumId++;
                        foreach (var song in album.Songs)
                        {
                            song.AlbumId = album.Id;
                            song.Id = songId++;
                            context.Songs.Add(song);
                        }
                        context.Albums.Add(album);
                    }
                    context.Artists.Add(artist);
                }

                context.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}
