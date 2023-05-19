using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieService.Repositories.RatingRepo;

namespace MovieService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingRepo ratingRepo;

        public RatingsController(IRatingRepo ratingRepository)
        {
            ratingRepo = ratingRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Rating>>> GetAllRatings()
        {
            try
            {
                return Ok(await ratingRepo.GetAllRatings());
            }
            catch (NullReferenceException e)
            {
                return StatusCode(404, e.Message);
            }
        }

        [HttpGet]
        [Route("{movieId:int}")]
        public async Task<ActionResult<Rating>> GetRatingByMovieId(int movieId)
        {
            try
            {
                var rating = await ratingRepo.GetRatingByMovieId(movieId);

                return Ok(rating);
            }
            catch (NullReferenceException e)
            {
                return StatusCode(404, e.Message);
            }
        }
    }
}