using DigitalMusicLibrary.Models;
using DigitalMusicLibrary.Repository;

namespace DigitalMusicLibrary.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public Task<bool> DeleteArtistAsync(int id)
        {
            return _artistRepository.DeleteArtistAsync(id);
        }

        public async Task<IEnumerable<Artist>> GetArtists()
        {
            return await _artistRepository.GetArtists();
        }

        public async Task<List<Artist>> SearchAsync(string searchTerm)
        {
            if (searchTerm.Count() == 0)
            {
                return await Task.FromResult(new List<Artist>());
            }
            else
            {
                return await _artistRepository.SearchAsync(searchTerm);
            }

        }

        public async Task<bool> UpdateArtistAsync(int id, Artist artist)
        {
            if (!(artist.Name.Length > 0))
             {
                return false;
            }

            return await _artistRepository.UpdateArtistAsync(id, artist);

        }
    }
}
