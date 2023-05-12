using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieService.Model;
using MovieService.Persistence;

namespace MovieService.Repositories.MovieRepo
{
    public class MovieRepo : IMovieRepo
    {
        private readonly MovieDbContext persistence;

        public MovieRepo(MovieDbContext movieDbContext){
            persistence = movieDbContext;
        }

        public async Task<IList<Movie>> GetMoviesAsync()
        {
            if (persistence.Movies != null)
            {
                var movieSet = await persistence.Movies.ToArrayAsync();
                return movieSet.ToList();
            }
            else
            {
                var testMovie = new Movie{
                    Id = 123,
                    Title = "Test",
                    Year = 2023
                };
                return new List<Movie>{testMovie};
            }
        }
        
        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            if (persistence.Movies != null)
            {
                return await persistence.Movies.FindAsync(id);
            }

            return new Movie {Id = id, Title = "Test", Year = 2023};
        }
    }
}