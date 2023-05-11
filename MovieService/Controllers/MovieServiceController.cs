using Microsoft.AspNetCore.Mvc;
using MovieService.Model;
using MovieService.Repositories.MovieRepo;

namespace MovieService.Controllers{
    [ApiController]
    [Route("[controller]")]
    public class MovieServiceController : ControllerBase
    {
        private IMovieRepo movieRepo;

        public MovieServiceController(IMovieRepo movieRepository){
            movieRepo = movieRepository;
        }

        [HttpGet]
        public Task<IList<Movie>> GetMovies()
        {
            //Get movies
            return movieRepo.GetMoviesAsync();
        }
    }
}
