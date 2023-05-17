using Microsoft.EntityFrameworkCore;

namespace MovieService.Persistence
{
    public class RatingDbContext : DbContext
    {
        public DbSet<Rating>? Ratings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = /app/movies.db");
        } 
    }
}