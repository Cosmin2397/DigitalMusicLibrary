using DigitalMusicLibrary.Models;
using DigitalMusicLibrary.Repository;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.Data;

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

        public async Task<Artist> GetArtistByIdAsync(int id)
        {
            if(id == 0)
            {
                return new Artist();
            }
            else
            {
                return await _artistRepository.GetArtistByIdAsync(id);
            }
        }

        public async Task<List<Artist>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return (List<Artist>)await _artistRepository.GetArtists();
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
