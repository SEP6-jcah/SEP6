using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieService.Repositories.MovieRepo
{
    public interface IMovieRepo
    {
        Task<IList<Movie>> GetAllMoviesAsync();
        Task<IList<Movie>> GetNext25MoviesAsync(int startIndex);
        Task<Movie> GetMovieByIdAsync(int id);
    }
}
