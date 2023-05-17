using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieService.Persistence;

namespace MovieService.Repositories.RatingRepo
{
    public class RatingRepo : IRatingRepo
    {
        private readonly RatingDbContext persistence;

        public RatingRepo(RatingDbContext ratingDbContext)
        {
            persistence = ratingDbContext;
        }

        public async Task<IList<Rating>> GetAllRatings()
        {
            if (persistence.Ratings != null)
            {
                var ratings = await persistence.Ratings.ToArrayAsync();
                return ratings.ToList();
            }

            throw new NullReferenceException("Failed to retrieve ratings.");
        }

        public async Task<Rating> GetRatingByMovieId(int movieId)
        {
            Rating? rating = null;
            if (persistence.Ratings != null)
            {
                rating = await persistence.Ratings.FindAsync(movieId);
            }

            if (rating == null)
            {
                throw new NullReferenceException($"Rating for movie with ID {movieId} not found.");
            }
            return rating;
        }
    }
}