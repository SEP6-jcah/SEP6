using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieService.Model;

namespace MovieService.Persistence{
    public class MovieDbContext : DbContext
    {

        public DbSet<Movie>? movies { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = /app/movies.db");
        } 
    }
}
