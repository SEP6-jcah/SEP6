using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieService.Model;
using MovieService.Repositories.MovieRepo;

namespace MovieService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private IMovieRepo movieRepo;

        public MoviesController(IMovieRepo movieRepository){
            movieRepo = movieRepository;
        }

        [HttpGet]
        public async Task<IList<Movie>> GetMovies()
        {
            //Get movies
            return await movieRepo.GetMoviesAsync();
        }
    }
}
