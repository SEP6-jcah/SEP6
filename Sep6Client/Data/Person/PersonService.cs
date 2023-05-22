using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Sep6Client.Data.DataHelper;
using Sep6Client.Data.DataHelper.Wrappers;
using Sep6Client.Data.Movies;

namespace Sep6Client.Data.Person
{
    public class PersonService : IPersonService
    {
        private readonly HttpClient client;
        private const string BaseUri = "https://api.themoviedb.org/3/";
        private const string ApiKey = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIzM2YxNmY4ZDRmYTE4ZDhmZTY2MTZlNDcyYWJhMjNhMCIsInN1YiI6IjY0NjVkNjA3MDA2YjAxMDEwNTg4Y2ZkNiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.RCfsqHoolmtYr-oCW7DLtmdlR1zqfeJz2NUkPvgBQZg";
        private readonly JsonSerializerOptions options;
        private readonly MoviesService moviesService = new ();

        public PersonService()
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

        private async Task<PersonListResult> GetPersonsAsync(int pageNr)
        {
            var response = await client.GetAsync($"{BaseUri}person/popular?page={pageNr}");
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }

            var stream = await response.Content.ReadAsStreamAsync();

            var httpResponse = JsonSerializer.Deserialize<PersonListResult>(stream, options);

            return httpResponse ?? throw new HttpRequestException("Unmarshalling persons http response failed.");
        }

        public async Task<IList<Model.Person>> GetBrowsePersonsAsync(int pageNr)
        {
            var response = await GetPersonsAsync(pageNr);
            var persons = new List<Model.Person>();

            try
            {
                persons = response.Persons.Select(PersonMapper.ToPerson).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\n{e.StackTrace}");
                throw new FormatException($"Failed to map persons: {e.Message}\n{e.StackTrace}");
            }

            return persons;
        }

        public async Task<IList<Model.Person>> GetCrewByMovieId(int movieId)
        {
            var movie = await moviesService.GetMovieByIdAsync(movieId);
            var crew = new List<Model.Person>();

            try
            {
                crew.AddRange(movie.Actors.Select(PersonMapper.ToPerson).ToList());
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\n{e.StackTrace}");
                throw new FormatException($"Failed to map crew from movie with ID {movieId}: {e.Message}\n{e.StackTrace}");
            }

            return crew;
        }

        private async Task<PersonResult> GetPersonAsync(int id)
        {
            var response = await client.GetAsync($"{BaseUri}person/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }
            
            var stream = await response.Content.ReadAsStreamAsync();

            var httpResponse = JsonSerializer.Deserialize<PersonResult>(stream, options);
            
            return httpResponse ?? throw new HttpRequestException("Unmarshalling person http response failed.");
        }

        public async Task<Model.Person> GetPersonByIdAsync(int personId)
        {
            var response = await GetPersonAsync(personId);

            var person = new Model.Person();
            try
            {
                person = PersonMapper.ToPerson(response);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\n{e.StackTrace}");
                throw new FormatException($"Failed to map person with ID {personId}: {e.Message}\n{e.StackTrace}");
            }

            return person;
        }
    }
}