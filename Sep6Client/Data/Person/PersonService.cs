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
using Sep6Client.Data.Movies;

namespace Sep6Client.Data.Person
{
    public class PersonService : IPersonService
    {
        private readonly HttpClient client;
        private readonly string baseUri;
        private readonly string apiKey;
        private readonly JsonSerializerOptions options;
        private MoviesService moviesService;

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
        }

        private async Task<PersonListResult> GetPersonsAsync(int pageNr)
        {
            var response = await client.GetAsync($"{baseUri}person/popular?page={pageNr}");
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }

            var stream = await response.Content.ReadAsStreamAsync();

            var httpResponse = JsonSerializer.Deserialize<PersonListResult>(stream, options);

            return httpResponse ?? throw new HttpRequestException("Unmarshalling persons http response failed.");
        }

        public async Task<PersonList> GetBrowsePersonsAsync(int pageNr)
        {
            var response = await GetPersonsAsync(pageNr);
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

        private async Task<int> GetResultPagesAsync(string movieTitle)
        {
            if (string.IsNullOrWhiteSpace(movieTitle))
                return 0;
            var response = await client.GetAsync($"{baseUri}search/person?query={movieTitle}");
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }
            
            var stream = await response.Content.ReadAsStreamAsync();

            var httpResponse = JsonSerializer.Deserialize<PersonListResult>(stream);

            return httpResponse?.NrOfPages ?? 0;
        }
        // private async Task<PersonListResult> GetCrewAsync(string movieTitle)
        // {
        //     var resultPageCount = await GetResultPagesAsync(movieTitle);
        //     var personList = new PersonListResult
        //     {
        //         Persons = new List<PersonResult>(),
        //         NrOfPages = 0
        //     };
        //
        //     for (var i = 0; i < resultPageCount; i++)
        //     {
        //         var response = await client.GetAsync($"{baseUri}search/person?query={movieTitle}");
        //         
        //         if (!response.IsSuccessStatusCode)
        //         {
        //             throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
        //         }
        //         
        //         var stream = await response.Content.ReadAsStreamAsync();
        //         var httpResponse = JsonSerializer.Deserialize<PersonListResult>(stream, options);
        //         
        //
        //         try
        //         {
        //             foreach (var person in httpResponse.Persons)
        //             {
        //                 personList.Persons.Add(person);
        //             }
        //         }
        //         catch (Exception e)
        //         {
        //             throw new HttpRequestException($"No crew found for {movieTitle}", e);
        //         }
        //     }
        //
        //     return personList;
        // }

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
    }
}