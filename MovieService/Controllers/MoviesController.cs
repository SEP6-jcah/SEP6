using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MovieService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepo movieRepo;

        public MoviesController(IMovieRepo movieRepository)
        {
            movieRepo = movieRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Movie>>> GetMoviesAsync([FromQuery] int startIndex)
        {
            try
            {
                return Ok(await movieRepo.GetNext25MoviesAsync(startIndex));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return StatusCode(418);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Movie>> GetMovieById(int id)
        {
            try
            {
                return Ok(await movieRepo.GetMovieByIdAsync(id));
            }
            catch (NullReferenceException e)
            {
                return StatusCode(404, e.Message);
            }
        }
    }
}
