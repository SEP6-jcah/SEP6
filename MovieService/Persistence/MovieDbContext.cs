using Microsoft.EntityFrameworkCore;
using MovieService.Model;

namespace MovieService.Persistence;

public class MovieDbContext : DbContext
{

    public DbSet<Movie> Movies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data Source = ..\movies.db");
    } 
}
