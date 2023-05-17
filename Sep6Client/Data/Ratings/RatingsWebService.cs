using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Sep6Client.Model;

namespace Sep6Client.Data.Ratings
{
    public class RatingsWebService : IRatingsWebService
    {
        private readonly HttpClient client;
        private readonly string uri = "http://movieservice:80/ratings";

        public RatingsWebService()
        {
            client = new HttpClient();
        }
        
        public async Task<IList<Rating>> GetAllRatings()
        {
            var response = await client.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }

            var ratingsAsJson = await response.Content.ReadAsStreamAsync();

            var ratings = JsonSerializer.Deserialize<IList<Rating>>(ratingsAsJson);

            return ratings ?? throw new FormatException("Unmarshalling ratings failed.");
        }

        public async Task<Rating> GetRatingForMovie(int movieId)
        {
            var response = await client.GetAsync($"{uri}/{movieId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }

            var ratingAsJson = await response.Content.ReadAsStreamAsync();

            var rating = JsonSerializer.Deserialize<Rating>(ratingAsJson);

            return rating ?? throw new FormatException("Unmarshalling ratings failed.");
        }
    }
}