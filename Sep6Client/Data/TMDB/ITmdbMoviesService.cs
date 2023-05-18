using System.Collections.Generic;
using System.Threading.Tasks;
using Sep6Client.Model;

namespace Sep6Client.Data.TMDB
{
    public interface ITmdbMoviesService
    {
        Task<IList<TmdbMovie>> GetBrowsingMoviesAsync();
        Task<TmdbMovie> GetMovieByIdAsync(int id);
    }
}