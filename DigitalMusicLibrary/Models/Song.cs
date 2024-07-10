﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalMusicLibrary.Models
{
    public class Song
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Length { get; set; }

        public int AlbumId { get; set; }
    }
}
