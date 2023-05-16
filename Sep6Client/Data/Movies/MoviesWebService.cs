using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Sep6Client.Model;

namespace Sep6Client.Data.Movies
{
    public class MoviesWebService : IMoviesWebService
    {
        private HttpClient client;
        private readonly string uri = "http://movieservice:80/movies";
        private readonly JsonSerializerOptions camelCase;
        private readonly JsonSerializerOptions caseInsensitive;

        public MoviesWebService()
        {
            client = new HttpClient();
            camelCase = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            caseInsensitive  = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }
        
        public async Task<IList<Movie>> GetMoviesAsync(int startIndex)
        {
            var response = await client.GetAsync($"{uri}?startIndex={startIndex}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }
            
            var moviesAsJson = await response.Content.ReadAsStreamAsync();
            
            var movies = JsonSerializer.Deserialize<IList<Movie>>(moviesAsJson, caseInsensitive);

            return movies ?? throw new FormatException("Unmarshalling movies failed.");
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var response = await client.GetAsync($"{uri}/{id}");

            var movieAsJson = await response.Content.ReadAsStringAsync();
            var movie = JsonSerializer.Deserialize<Movie>(movieAsJson);
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }

            return movie ?? throw new FormatException($"Unmarshalling movie with ID {id} failed.");
        }
    }
}