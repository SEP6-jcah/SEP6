using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieService.Repositories.RatingRepo
{
    public interface IRatingRepo
    {
        Task<IList<Rating>> GetAllRatings();
        Task<Rating> GetRatingByMovieId(int movieId);
    }
}