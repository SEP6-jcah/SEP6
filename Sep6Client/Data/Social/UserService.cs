using System.Text.Json;
using Sep6Client.Data.DataHelper;
using Sep6Client.Model;
using Microsoft.Extensions.Options;
using Firebase.Database;
using Firebase.Database.Query;

namespace Sep6Client.Data.Social{
    public class UserService : IUserService
    {
        private readonly FirebaseClient firebaseClient;
        private string baseUri;
        private string dbResource = "Users";
        private readonly JsonSerializerOptions options;

        public UserService(IOptions<RtdbSettings> rtdbSettings)
        {
            baseUri = rtdbSettings.Value.BaseUri;
            firebaseClient = new FirebaseClient(baseUri);
            options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        }
        public async Task<RegisteredUser> GetUserByEmailAsync(string email)
        {
            var users = await firebaseClient
                .Child(dbResource)
                .OnceAsync<RegisteredUser>();
            
            var user = users.FirstOrDefault(u => u.Object.Email == email);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            return user.Object;
        }

        public async Task UpdateUserAsync(RegisteredUser user)
        {
            var emailKey = user.Email.Replace(".", "_"); 
            await firebaseClient
                .Child(dbResource)
                .Child(emailKey)
                .PutAsync(user);
        }

        public async Task CreateUserAsync(RegisteredUser user)
        {
            try
            {
            var emailKey = user.Email.Replace(".", "_"); 
            await firebaseClient.Child(dbResource)
                .Child(emailKey)
                .PutAsync(user);
            }
            catch(Exception e)
            {
            await Console.Out.WriteLineAsync(e.StackTrace);
            }
        }


        public async Task DeleteUserAsync(string email)
        {
            var user = await GetUserByEmailAsync(email);

            if (user != null)
            {
                await firebaseClient
                    .Child(dbResource)
                    .Child(user.Email)
                    .DeleteAsync();
            }
        }

        public async Task AddMovieToFavoritesAsync(string userEmail, int movieId)
        {
            var user = await GetUserByEmailAsync(userEmail);

            if (user != null)
            {
                user.FavouriteMovies.Add(movieId.ToString());
                await UpdateUserAsync(user);
            }
            else
            {
                throw new Exception("User not found");
            }
        }
    }
}