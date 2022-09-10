using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiVideoGames.Models
{
    public class VideoGamesContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<GameGenre> GamesGenres { get; set; }
        public VideoGamesContext(DbContextOptions<VideoGamesContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
