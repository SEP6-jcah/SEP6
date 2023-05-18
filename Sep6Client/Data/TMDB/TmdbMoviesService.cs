using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Sep6Client.Data.TMDB.DataHelper;
using Sep6Client.Model;

namespace Sep6Client.Data.TMDB
{
    public class TmdbMoviesService : ITmdbMoviesService
    {
        private readonly HttpClient client;
        private const string BaseUri = "https://api.themoviedb.org/3/";
        private const string ApiKey = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIzM2YxNmY4ZDRmYTE4ZDhmZTY2MTZlNDcyYWJhMjNhMCIsInN1YiI6IjY0NjVkNjA3MDA2YjAxMDEwNTg4Y2ZkNiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.RCfsqHoolmtYr-oCW7DLtmdlR1zqfeJz2NUkPvgBQZg";
        private readonly JsonSerializerOptions options;
        
        public TmdbMoviesService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        }

        private async Task<TmdbMovieListResult> GetMoviesAsync()
        {
            var response = await client.GetAsync($"{BaseUri}discover/movie?include_adult=false&include_video=false&page=1&sort_by=popularity.desc");
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }

            var stream = await response.Content.ReadAsStreamAsync();

            var httpResponse = JsonSerializer.Deserialize<TmdbMovieListResult>(stream, options);

            return httpResponse ?? throw new HttpRequestException("Unmarshalling TMDB movies http response failed.");
        }

        public async Task<IList<TmdbMovie>> GetBrowsingMoviesAsync()
        {
            var response = await GetMoviesAsync();

            var movies = new List<TmdbMovie>();

            try
            {
                movies = response.Movies.Select(TmdbMovieMapper.ToTmdbMovie).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to map movies: {e.Message}\n{e.StackTrace}");
                throw new FormatException($"Failed to map movies: {e.Message}\n{e.StackTrace}");
            }

            return movies;
        }

        private async Task<TmdbMovieResult> GetMovieAsync(int id)
        {
            var response = await client.GetAsync($"{BaseUri}/movie/{id}");
                        
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }
            
            var stream = await response.Content.ReadAsStreamAsync();

            var httpResponse = JsonSerializer.Deserialize<TmdbMovieResult>(stream, options);
            
            return httpResponse ?? throw new HttpRequestException("Unmarshalling TMDB movies http response failed.");
        }
        public async Task<TmdbMovie> GetMovieByIdAsync(int id)
        {
            var response = await GetMovieAsync(id);

            var movie = new TmdbMovie();
            try
            {
                movie = TmdbMovieMapper.ToTmdbMovie(response);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\n{e.StackTrace}");
                throw new FormatException($"Failed to map movie with ID {id}: {e.Message}\n{e.StackTrace}");
            }

            return movie;
        }
    }
}