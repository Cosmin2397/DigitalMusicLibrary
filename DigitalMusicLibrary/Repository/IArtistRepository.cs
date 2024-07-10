using DigitalMusicLibrary.Models;

namespace DigitalMusicLibrary.Repository
{
    public interface IArtistRepository
    {
        Task<IEnumerable<Artist>> GetArtists();

        Task<Artist> GetArtistByIdAsync(int id);

        Task<List<Artist>> SearchAsync(string searchTerm);

        Task<bool> UpdateArtistAsync(int id, Artist artist);

        Task<bool> DeleteArtistAsync(int id);
    }
}
