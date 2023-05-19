using Microsoft.EntityFrameworkCore;

namespace MovieService.Persistence
{
    public class MovieDbContext : DbContext
    {
        public DbSet<Movie>? Movies { get; set; }
        public DbSet<Rating>? Ratings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = /app/movies.db");
        } 
    }
}
