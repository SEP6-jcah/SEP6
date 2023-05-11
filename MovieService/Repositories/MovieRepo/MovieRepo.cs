using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            // if (persistence.movies != null)
            // {
            //     return persistence.movies.ToList();
            // }
            // else
            // {
                var testMovie = new Movie{
                    Id = 123,
                    Title = "Test",
                    Year = 2023
                };
                return new List<Movie>{testMovie};
            // }
        }


        // public async Task<Movie> GetMovieByIdAsync(int Id)
        // {
        //     return await persistence.Movies.FindAsync(Id);
        // }
    }
}