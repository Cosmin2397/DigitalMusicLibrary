using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalMusicLibrary.Models
{
    public class Album
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int ArtistId { get; set; }

        [NotMapped]
        public List<Song> Songs { get; set; }
    }
}
