using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Sep6Client.Data.DataHelper;
using Sep6Client.Data.DataHelper.Mappers;
using Sep6Client.Data.DataHelper.Wrappers;
using Sep6Client.Model;
using Microsoft.Extensions.Options;

namespace Sep6Client.Data.Crew
{
    public class CastAndCrewService : ICastAndCrewService
    {
        private readonly HttpClient client;
        private string baseUri;
        private string apiKey;
        private readonly JsonSerializerOptions options;

        public CastAndCrewService(IOptions<TmdbSettings> tmdbSettings)
        {
            client = new HttpClient();
            baseUri = tmdbSettings.Value.BaseUri;
            apiKey = tmdbSettings.Value.ApiKey;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        }

        private async Task<CastAndCrewListResult> GetCastAndCrewAsync(int movieId)
        {
            var response = await client.GetAsync($"{baseUri}movie/{movieId}/credits");
            Console.WriteLine($"Sending cast and crew query to: {baseUri}movie/{movieId}/credits");
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }

            var stream = await response.Content.ReadAsStreamAsync();

            var httpResponse = JsonSerializer.Deserialize<CastAndCrewListResult>(stream, options);

            return httpResponse ?? throw new HttpRequestException("Unmarshalling cast and crew http response failed.");
        }

        public async Task<IList<CrewMember>> GetCrewByMovieIdAsync(int id)
        {
            var response = await GetCastAndCrewAsync(id);

            var crew = new List<CrewMember>();

            try
            {
                crew.AddRange(response.Crew.Select(CrewMemberMapper.ToCrewMember));
            }
            catch (Exception e)
            {
                throw new FormatException($"Failed to map crew members: {e.Message}\n{e.StackTrace}", e);
            }

            return crew;
        }

        public async Task<IList<Actor>> GetActorsByMovieIdAsync(int id)
        {
            var response = await GetCastAndCrewAsync(id);

            var actors = new List<Actor>();

            try
            {
                actors = response.Actors.Select(ActorMapper.ToActor).ToList();
            }
            catch (Exception e)
            {
                throw new FormatException($"Failed to actors: {e.Message}\n{e.StackTrace}", e);
            }

            return actors;
        }
    }
}