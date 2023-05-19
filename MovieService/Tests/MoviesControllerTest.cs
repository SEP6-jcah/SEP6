using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieService.Controllers;
using MovieService.Model;
using MovieService.Repositories.MovieRepo;
using Xunit;

namespace MovieService.Tests
{
    public class MoviesControllerTests
    {
        [Fact]
        public async Task GetMovies_ReturnsListOfMovies()
        {
            // Arrange
            var expectedMovies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Dodgeball", Year = 2004},
                new Movie { Id = 2, Title = "The Princess Bride", Year = 1987},
                new Movie { Id = 3, Title = "Transformers", Year = 2007}
            };
        
            var mockMovieService = new Mock<IMovieRepo>();
            mockMovieService.Setup(service => service.GetNext25MoviesAsync(0)).ReturnsAsync(expectedMovies);
        
            var controller = new MoviesController(mockMovieService.Object);
        
            // Act
            var response = await controller.GetMoviesAsync(0);
            var result = (response.Result as OkObjectResult).Value as IList<Movie>;
        
            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Movie>>(result);
            Assert.Equal(expectedMovies.Count, result.Count);
            Assert.Equal("Dodgeball", result.First().Title);
        }

        [Theory]
        [InlineData(0)]
        public async Task GetMovies_StartIndex0_Returns25Movies(int startIndex)
        {
            // Arrange
            var expectedMovies = new List<Movie>();

            for (var i = 0; i < 25; i++)
            {
                expectedMovies.Add(new Movie {Id = i, Title = $"Test title {i+1}", Year = 1950 + i});
            }
            
            var mockMovieService = new Mock<IMovieRepo>();
            mockMovieService.Setup(service => service.GetNext25MoviesAsync(startIndex)).ReturnsAsync(expectedMovies);
            
            var controller = new MoviesController(mockMovieService.Object);
            
            // Act
            var response = await controller.GetMoviesAsync(startIndex);
            var result = (response.Result as OkObjectResult).Value as IList<Movie>;
            
            // Assert
            Assert.NotNull(result);
            Assert.Equal(25, result.Count);
            Assert.Equal("Test title 1", result.First().Title);
            Assert.Equal("Test title 25", result.Last().Title);
        }
    }
}
