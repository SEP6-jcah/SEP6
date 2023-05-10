using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieService.Model;
using MovieService.Persistence;

namespace MovieService.Repositories.MovieRepo{
    public class MovieRepo : IMovieRepo
    {
        private readonly MovieDbContext persistence;

        public async Task<IList<Movie>> GetMoviesAsync()
        {
            return persistence.Movies.ToList();
        }

        public async Task<Movie> GetMovieByIdAsync(int Id)
        {
            return await persistence.Movies.FindAsync(Id);
        }
    }
}