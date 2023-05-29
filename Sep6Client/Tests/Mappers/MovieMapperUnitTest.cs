using NUnit.Framework;
using Sep6Client.Data.DataHelper.Mappers;
using Sep6Client.Data.DataHelper.Wrappers;

namespace Sep6Client.UnitTest.Mappers
{
    [TestFixture]
    public class MovieMapperUnitTest
    {
        private MovieResult wrapper;

        [SetUp]
        public void Setup()
        {
            wrapper = new MovieResult();
        }

        [Test]
        public void ToMovie_EmptyWrapper_ReturnsEmptyMovie()
        {
            // Arrange
            
            // Act
            var result = MovieMapper.ToMovie(wrapper);
            
            // Assert
            Assert.AreEqual(0, result.Id);
            Assert.AreEqual(0.0, result.Rating);
            Assert.AreEqual(0, result.Votes);
            Assert.IsEmpty(result.Actors);
            Assert.IsEmpty(result.Crew);
            Assert.IsEmpty(result.Directors);
            Assert.IsNull(result.Title);
            Assert.IsNull(result.Description);
            Assert.IsNull(result.Language);
            Assert.IsNull(result.Poster);
            Assert.IsNull(result.ReleaseDate);
        }
        
        [Test]
        public void ToMovie_ValidMovieWrapper_ReturnsTrue()
        {
            // Arrange
            wrapper = new MovieResult
            {
                Id = 666,
                Title = "The End",
                Description = "Yes.",
                Poster = "No.",
                Rating = 6.66,
                Votes = 999,
                ReleaseDate = "Maybe.",
                Language = "Enochian"
            };
            
            // Act
            var result = MovieMapper.ToMovie(wrapper);
            
            // Assert
            Assert.AreEqual(666, result.Id);
            Assert.AreEqual(6.66, result.Rating);
            Assert.AreEqual(999, result.Votes);
            Assert.IsEmpty(result.Actors);
            Assert.IsEmpty(result.Crew);
            Assert.IsEmpty(result.Directors);
            Assert.AreEqual("The End", result.Title);
            Assert.AreEqual("Yes.", result.Description);
            Assert.AreEqual("Enochian",result.Language);
            Assert.AreEqual("No.", result.Poster);
            Assert.AreEqual("Maybe.", result.ReleaseDate);
        }
    }
}