using DigitalMusicLibrary.Models;

namespace DigitalMusicLibrary.Services
{
    public class ArtistService : IArtistService
    {
        private readonly
        public ArtistService()
        {
            
        }
        public Task<bool> DeleteArtistAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Artist>> GetArtists()
        {
            throw new NotImplementedException();
        }

        public Task<List<Artist>> SearchAsync(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateArtistAsync(int id, Artist artist)
        {
            throw new NotImplementedException();
        }
    }
}
