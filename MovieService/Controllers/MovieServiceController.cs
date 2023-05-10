using Microsoft.AspNetCore.Mvc;
using MovieService.Model;
using MovieService.Repositories.Interfaces;

namespace MovieService.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieServiceController : ControllerBase
{
    private IMovieRepo MovieRepo;

    [HttpGet(Name = "GetMovie")]
    public IEnumerable<Movie> GetMovies()
    {
        //Get movies
        return (IEnumerable<Movie>)MovieRepo.GetMovieAsync();
    }
}
