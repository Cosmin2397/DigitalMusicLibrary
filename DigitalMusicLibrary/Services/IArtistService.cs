using DigitalMusicLibrary.Models;

namespace DigitalMusicLibrary.Services
{
    public interface IArtistService
    {
        Task<IEnumerable<Artist>> GetArtists();

        Task<Artist> GetArtistByIdAsync(int id);

        Task<List<Artist>> SearchAsync(string searchTerm);

        Task<bool> UpdateArtistAsync(int id, Artist artist);

        Task<bool> DeleteArtistAsync(int id);
    }
}
