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
                return await _context.Artists.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }

        public async Task<List<Artist>> SearchAsync(string searchTerm)
        {
            searchTerm = searchTerm.ToLower();
            return await _context.Artists
                .Where(a => a.Name.ToLower().Contains(searchTerm) ||
                            a.Albums.Any(al => al.Title.ToLower().Contains(searchTerm) ||
                                               al.Songs.Any(s => s.Title.ToLower().Contains(searchTerm))))
                .Include(a => a.Albums)
                .ThenInclude(al => al.Songs)
                .ToListAsync();
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
