using DigitalMusicLibrary.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace DigitalMusicLibrary.Data
{

    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Artists.Any())
            {
                return;   // DB has been seeded
            }

            var json = File.ReadAllText("C:/Users/Cosmin Moisa/Desktop/data.json");
            var artistData = JsonConvert.DeserializeObject<List<Artist>>(json);

            foreach (var artistDataItem in artistData)
            {
                var artist = new Artist
                {
                    Name = artistDataItem.Name,
                    Albums = artistDataItem.Albums.Select(albumData => new Album
                    {
                        Title = albumData.Title,
                        Description = albumData.Description,
                        Songs = albumData.Songs.Select(songData => new Song
                        {
                            Title = songData.Title,
                            Length = songData.Length
                        }).ToList()
                    }).ToList()
                };

                context.Artists.Add(artist);
            }

            context.SaveChanges();
        }
    }
}
