using System.Collections.Generic;
using System.Threading.Tasks;
using Sep6Client.Model;

namespace Sep6Client.Data.Social
{
    public interface IUserService
    {
        Task<RegisteredUser> GetUserByEmailAsync(string email);
        Task<RegisteredUser> GetUserByUsernameAsync(string username);
        Task UpdateUserAsync(RegisteredUser user);
        Task CreateUserAsync(RegisteredUser user);
        Task DeleteUserAsync(string email);
        Task AddMovieToFavoritesAsync(string userEmail, int movieId);
        Task<bool> IsUsernameTakenAsync(string username);
    }
}