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
using Sep6Client.Data.DataHelper.Search;
using Sep6Client.Data.Movies;

namespace Sep6Client.Data.Person
{
    public class PersonService : IPersonService
    {
        private readonly HttpClient client;
        private readonly string baseUri;
        private readonly string apiKey;
        private readonly JsonSerializerOptions options;
        private IMoviesService moviesService;
        private PersonQueryHelper queryHelper;

        public PersonService(IOptions<TmdbSettings> tmdbSettings)
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
            moviesService = new MoviesService(tmdbSettings);
            queryHelper = new PersonQueryHelper();
        }

        private async Task<PersonListResult> GetPersonsAsync(string query)
        {
            Console.WriteLine($"\n\n\nSending query: {query}");
            var response = await client.GetAsync(baseUri + query);
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }

            var stream = await response.Content.ReadAsStreamAsync();

            var httpResponse = JsonSerializer.Deserialize<PersonListResult>(stream, options);

            return httpResponse ?? throw new HttpRequestException("Unmarshalling persons http response failed.");
        }

        public async Task<PersonList> GetBrowsePersonsAsync(Dictionary<SearchFilterOptions, string> criteria)
        {
            var query = queryHelper.GetBrowseQuery(criteria);
            var response = await GetPersonsAsync(query);
            
            var persons = new PersonList
            {
                Persons = new List<Model.Person>(),
                NrOfPages = 0,
                NrOfResults = 0
            };

            try
            {
                foreach (var person in response.Persons)
                {
                    persons.Persons.Add(PersonMapper.ToPerson(person));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\n{e.StackTrace}");
                throw new FormatException($"Failed to map persons: {e.Message}\n{e.StackTrace}");
            }

            return persons;
        }

        private async Task<PersonResult> GetPersonAsync(int id)
        {
            var response = await client.GetAsync($"{baseUri}person/{id}");
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
                person.KnownFor = await moviesService.GetMoviesByPersonIdAsync(personId);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\n{e.StackTrace}");
                throw new FormatException($"Failed to map person with ID {personId}: {e.Message}\n{e.StackTrace}");
            }

            return person;
        }

        public async Task<PersonList> GetFilteredPersonsAsync(Dictionary<SearchFilterOptions, string> criteria)
        {
            var query = queryHelper.GetSearchQuery(criteria);

            var response = await GetPersonsAsync(query);
            
            var persons = new PersonList
            {
                Persons = new List<Model.Person>(),
                NrOfPages = 0,
                NrOfResults = 0
            };

            try
            {
                foreach (var person in response.Persons)
                {
                    persons.Persons.Add(PersonMapper.ToPerson(person));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\n{e.StackTrace}");
                throw new FormatException($"Failed to map persons: {e.Message}\n{e.StackTrace}");
            }

            return persons;
        }
    }
}