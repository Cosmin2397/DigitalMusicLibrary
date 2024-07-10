using DigitalMusicLibrary.Data;
using DigitalMusicLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalMusicLibrary.Repository
{
    public class ArtistRepository : IArtistRepository
    {
        public readonly AppDbContext _context;

        public ArtistRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteArtistAsync(int id)
        {
            if (id > 0)
            {
                if (!ArtistExists(id))
                {
                    return false;
                }
                else
                {
                    var artist = await _context.Artists.FirstOrDefaultAsync(a => a.Id == id);
                    try
                    {
                         _context.Artists.Remove(artist);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
            return false;
        }

        public async Task<IEnumerable<Artist>> GetArtists()
        {
            try
            {
                return await _context.Artists
                .Include(a => a.Albums)
                .ThenInclude(album => album.Songs)
                .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }

        public async Task<Artist> GetArtistByIdAsync(int id)
        {
            try
            {
                Artist artist = await _context.Artists
                    .Include(a => a.Albums)
                        .ThenInclude(album => album.Songs)
                    .FirstOrDefaultAsync(a => a.Id == id);

                return artist;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching the artist: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Artist>> SearchAsync(string searchTerm)
        {
            searchTerm = searchTerm.ToLower();

            var query = _context.Artists
                .Include(a => a.Albums)
                .ThenInclude(al => al.Songs)
                .AsQueryable();

            var result = await query.Where(a =>
                a.Name.ToLower().Contains(searchTerm) ||
                a.Albums.Any(al => al.Title.ToLower().Contains(searchTerm)) ||
                a.Albums.Any(al => al.Songs.Any(s => s.Title.ToLower().Contains(searchTerm)))
            ).ToListAsync();

            return result.Select(artist => new Artist
            {
                Id = artist.Id,
                Name = artist.Name,
                Albums = artist.Albums.Where(album =>
                    artist.Name.ToLower().Contains(searchTerm) ||
                    album.Title.ToLower().Contains(searchTerm) ||
                    album.Songs.Any(song => song.Title.ToLower().Contains(searchTerm))
                ).Select(album => new Album
                {
                    Id = album.Id,
                    Title = album.Title,
                    Description = album.Description,
                    ArtistId = album.ArtistId,
                    Songs = album.Songs.Where(song =>
                        artist.Name.ToLower().Contains(searchTerm) ||
                        album.Title.ToLower().Contains(searchTerm) ||
                        song.Title.ToLower().Contains(searchTerm)
                    ).ToList()
                }).ToList()
            }).ToList();
        }

        public async Task<bool> UpdateArtistAsync(int id, Artist artist)
        {
            if(id <= 0 || artist == null)
            {
                return false;
            }
            else
            {
                if(!ArtistExists(id))
                {
                    return false;
                }
                else
                {
                    var dbArtist = await _context.Artists.FirstOrDefaultAsync(a => a.Id == id);
                    dbArtist.Name = artist.Name;
                    dbArtist.Albums = artist.Albums;
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
        }


        public bool ArtistExists(int id)
        {
            return _context.Artists.Any(a => a.Id == id);
        }
    }
}
