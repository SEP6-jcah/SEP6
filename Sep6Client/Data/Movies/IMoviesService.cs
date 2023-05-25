using System.Collections.Generic;
using System.Threading.Tasks;
using Sep6Client.Data.DataHelper;
using Sep6Client.Model;

namespace Sep6Client.Data.Movies
{
    public interface IMoviesService
    {
        Task<MovieList> GetBrowsingMoviesAsync(Dictionary<SearchFilterOptions, string> filters);
        Task<MovieList> GetFilteredMoviesAsync(Dictionary<SearchFilterOptions, string> searchCriteria);
        Task<Movie> GetMovieByIdAsync(int id);
    }
}