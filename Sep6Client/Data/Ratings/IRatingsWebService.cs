using System.Collections.Generic;
using System.Threading.Tasks;
using Sep6Client.Model;

namespace Sep6Client.Data.Ratings
{
    public interface IRatingsWebService
    {
        Task<IList<Rating>> GetAllRatings();
        Task<Rating> GetRatingForMovie(int movieId);
    }
}