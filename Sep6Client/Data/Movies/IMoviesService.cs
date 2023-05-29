using System.Collections.Generic;
using System.Threading.Tasks;
using Sep6Client.Model;

namespace Sep6Client.Data.Movies
{
    public interface IMoviesService
    {
        Task<IList<Movie>> GetBrowsingMoviesAsync(int pageNr);
        Task<Movie> GetMovieByIdAsync(int id);
    }
}