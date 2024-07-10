using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalMusicLibrary.Models
{
    public class Album
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Album title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Album description is required")]
        public string Description { get; set; }

        public int ArtistId { get; set; }

        [NotMapped]
        public List<Song> Songs { get; set; }

        [NotMapped]
        public bool ShowSongs { get; set; }
    }
}
