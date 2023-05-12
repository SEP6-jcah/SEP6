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
            mockMovieService.Setup(service => service.GetMoviesAsync()).ReturnsAsync(expectedMovies);

            var controller = new MoviesController(mockMovieService.Object);

            // Act
            var result = await controller.GetMovies();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Movie>>(result);
            Assert.Equal(expectedMovies.Count, result.Count);
            Assert.Equal("Dodgeball", result.First().Title);
        }

    }
}
