using System.Collections.Generic;
using System.Threading.Tasks;
using Sep6Client.Model;

namespace Sep6Client.Data.Social{
    public interface IUserService
    {
        Task<RegisteredUser> GetUserByEmailAsync(string email);
        Task UpdateUserAsync(RegisteredUser user);
        Task CreateUserAsync(RegisteredUser user);
        Task DeleteUserAsync(string email);
        Task AddMovieToFavoritesAsync(string userEmail, int movieId);
    }
}