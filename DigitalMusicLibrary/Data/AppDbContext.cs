using DigitalMusicLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;

namespace DigitalMusicLibrary.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Artist>()
                .HasMany(a => a.Albums)
                .WithOne()
                .HasForeignKey(a => a.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Album>()
                .HasMany(a => a.Songs)
                .WithOne()
                .HasForeignKey(s => s.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

