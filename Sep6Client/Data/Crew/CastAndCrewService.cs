using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Sep6Client.Data.DataHelper.Mappers;
using Sep6Client.Data.DataHelper.Wrappers;
using Sep6Client.Model;

namespace Sep6Client.Data.Crew
{
    public class CastAndCrewService : ICastAndCrewService
    {
        private readonly HttpClient client;
        private const string BaseUri = "https://api.themoviedb.org/3/";
        private const string ApiKey = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIzM2YxNmY4ZDRmYTE4ZDhmZTY2MTZlNDcyYWJhMjNhMCIsInN1YiI6IjY0NjVkNjA3MDA2YjAxMDEwNTg4Y2ZkNiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.RCfsqHoolmtYr-oCW7DLtmdlR1zqfeJz2NUkPvgBQZg";
        private readonly JsonSerializerOptions options;
        //https://api.themoviedb.org/3/movie/640146/credits
        public CastAndCrewService()
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

        private async Task<CastAndCrewListResult> GetCastAndCrewAsync(int movieId)
        {
            var response = await client.GetAsync($"{BaseUri}movie/{movieId}/credits");
            Console.WriteLine($"Sending cast and crew query to: {BaseUri}movie/{movieId}/credits");
            
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
                // Console.WriteLine($"\n\n\n__________Count of crew l49: {response.Crew.Count}__________\n\n\n");
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