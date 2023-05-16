using System.Collections.Generic;
using System.Threading.Tasks;
using Sep6Client.Model;

namespace Sep6Client.Data.Movies
{
    public interface IMoviesWebService
    {
        Task<IList<Movie>> GetMoviesAsync(int startIndex);
        Task<Movie> GetMovieByIdAsync(int id);
    }
}