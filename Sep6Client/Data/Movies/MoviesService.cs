using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Sep6Client.Data.Crew;
using Sep6Client.Data.DataHelper;
using Sep6Client.Data.DataHelper.Mappers;
using Sep6Client.Data.DataHelper.Wrappers;
using Sep6Client.Model;

namespace Sep6Client.Data.Movies
{
    public class MoviesService : IMoviesService
    {
        private readonly HttpClient client;
        private const string BaseUri = "https://api.themoviedb.org/3/";
        private const string ApiKey = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIzM2YxNmY4ZDRmYTE4ZDhmZTY2MTZlNDcyYWJhMjNhMCIsInN1YiI6IjY0NjVkNjA3MDA2YjAxMDEwNTg4Y2ZkNiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.RCfsqHoolmtYr-oCW7DLtmdlR1zqfeJz2NUkPvgBQZg";
        private readonly JsonSerializerOptions options;
        private readonly QueryHelper queryHelper;
        private CastAndCrewService castAndCrewService = new();
        
        public MoviesService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
            queryHelper = new QueryHelper();
        }

        private async Task<MovieListResult> GetMoviesAsync(string query)
        {
            var response = await client.GetAsync(BaseUri + query);
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }

            var stream = await response.Content.ReadAsStreamAsync();

            var httpResponse = JsonSerializer.Deserialize<MovieListResult>(stream, options);

            return httpResponse ?? throw new HttpRequestException("Unmarshalling movies http response failed.");
        }

        public async Task<MovieList> GetBrowsingMoviesAsync(Dictionary<SearchFilterOptions, string> filters)
        {
            var query = queryHelper.GetBrowseQuery(filters);
            // Console.WriteLine($"Sending query to: {BaseUri}{query}");

            var response = await GetMoviesAsync(query);
            var movies = new MovieList
            {
                Movies = new List<Movie>(),
                NrOfPages = response.NrOfPages,
                NrOfResults = response.NrOfMovies
            };

            try
            {
                movies.Movies = response.Movies.Select(MovieMapper.ToMovie).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to map movies: {e.Message}\n{e.StackTrace}");
                throw new FormatException($"Failed to map movies: {e.Message}\n{e.StackTrace}");
            }

            // Console.WriteLine($"\n___________\nNr of pages: {movies.NrOfPages}\n___________\n");
            return movies;
        }

        public async Task<MovieList> GetFilteredMoviesAsync(Dictionary<SearchFilterOptions, string> searchCriteria)
        {
            var query = queryHelper.GetSearchQuery(searchCriteria);
            // Console.WriteLine($"Sending query to: {BaseUri}{query}\n--------------\n");

            var response = await GetMoviesAsync(query);
            var movies = new MovieList
            {
                Movies = new List<Movie>(),
                NrOfPages = response.NrOfPages,
                NrOfResults = response.NrOfMovies
            };

            try
            {
                movies.Movies = response.Movies.Select(MovieMapper.ToMovie).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to map movies: {e.Message}\n{e.StackTrace}");
                throw new FormatException($"Failed to map movies: {e.Message}\n{e.StackTrace}");
            }

            // Console.WriteLine($"\n___________\nNr of pages: {movies.NrOfPages}\n___________\n");
            return movies;
        }

        private async Task<MovieResult> GetMovieAsync(int id)
        {
            var response = await client.GetAsync($"{BaseUri}movie/{id}");
                        
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }
            
            var stream = await response.Content.ReadAsStreamAsync();

            var httpResponse = JsonSerializer.Deserialize<MovieResult>(stream, options);
            
            return httpResponse ?? throw new HttpRequestException("Unmarshalling movies http response failed.");
        }
        
        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var response = await GetMovieAsync(id);

            var movie = new Movie();
            try
            {
                movie = MovieMapper.ToMovie(response);
                // Adding crew & cast
                movie.Actors = await castAndCrewService.GetActorsByMovieIdAsync(id);
                movie.Crew = await castAndCrewService.GetCrewByMovieIdAsync(id);
                // Singling out the directors
                foreach (var person in movie.Crew)
                {
                    if (!person.Job.Equals("Director")) continue;
                
                    movie.Directors.Add(person);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\n{e.StackTrace}");
                throw new FormatException($"Failed to map movie with ID {id}: {e.Message}\n{e.StackTrace}", e);
            }

            return movie;
        }
    }
}