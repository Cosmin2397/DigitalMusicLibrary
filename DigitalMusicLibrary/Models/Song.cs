using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalMusicLibrary.Models
{
    public class Song
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Song title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Song length is required")]
        public string Length { get; set; }

        public int AlbumId { get; set; }
    }
}
