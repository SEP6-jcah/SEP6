using System.Collections.Generic;
using System.Threading.Tasks;
using MovieService.Model;

namespace MovieService.Repositories.MovieRepo
{
    public interface IMovieRepo
    {
        Task<IList<Movie>> GetMoviesAsync();
        //Task<Movie> GetMovieByIdAsync(int Id);
    }
}
