using System.Collections.Generic;
using System.Threading.Tasks;
using Sep6Client.Data.DataHelper.Search;
using Sep6Client.Model;

namespace Sep6Client.Data.Movies
{
    public interface IMoviesService
    {
        Task<MovieList> GetBrowsingMoviesAsync(Dictionary<SearchFilterOptions, string> filters);
        Task<MovieList> GetFilteredMoviesAsync(Dictionary<SearchFilterOptions, string> searchCriteria);
        Task<Movie> GetMovieByIdAsync(int id);
        Task<CreditList> GetMoviesByPersonIdAsync(int id);
        Task<MovieList> GetPopularMoviesAsync();
        Task<MovieList> GetCurrentMoviesAsync();
        Task<MovieList> GetHighestRatedMoviesAsync();
        Task<MovieList> GetUpcomingMoviesAsync();
    }
}