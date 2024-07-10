using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalMusicLibrary.Models
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [NotMapped]
        public List<Album> Albums { get; set; }
    }
}
