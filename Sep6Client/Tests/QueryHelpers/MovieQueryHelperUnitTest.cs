using System.Collections.Generic;
using NUnit.Framework;
using Sep6Client.Data.DataHelper.Search;

namespace Sep6Client.UnitTest.QueryHelpers
{
    [TestFixture]
    public class MovieQueryHelperUnitTest
    {
        private MovieQueryHelper uut;
        private Dictionary<SearchFilterOptions, string> criteria;
        private string[] sortByOptions = {"Popularity", "Rating"};

        [SetUp]
        public void Setup()
        {
            uut = new MovieQueryHelper();
            criteria = new Dictionary<SearchFilterOptions, string>
            {
                {SearchFilterOptions.Text, ""},
                {SearchFilterOptions.PageNr, ""},
                {SearchFilterOptions.SortBy, ""},
                {SearchFilterOptions.SortOrder, ""}
            };
        }

        [Test]
        public void GetSortByOptions_ReturnsTrue()
        {
            // Arrange
            
            // Act
            
            // Assert
            Assert.AreEqual(MovieQueryHelper.GetSortByOptions(), sortByOptions);
        }

        [Test]
        public void GetBrowseQuery_NoSetCriteria_ReturnsTrue()
        {
            // Arrange
            
            // Act
            var result = uut.GetBrowseQuery(criteria);

            // Assert
            Assert.AreEqual("discover/movie?include_adult=false&include_video=false&sort_by=popularity.desc", result);
        }
        
        [Test]
        public void GetBrowseQuery_WithIrrelevantSetCriteria_ReturnsTrue()
        {
            // Arrange
            criteria[SearchFilterOptions.Text] = "Don't Care.";
            
            // Act
            var result = uut.GetBrowseQuery(criteria);

            // Assert
            Assert.AreEqual("discover/movie?include_adult=false&include_video=false&sort_by=popularity.desc", result);
        }
        
        [Test]
        public void GetBrowseQuery_WithPageNumber_ReturnsTrue()
        {
            // Arrange
            criteria[SearchFilterOptions.PageNr] = "100";

            // Act
            var result = uut.GetBrowseQuery(criteria);

            // Assert
            Assert.AreEqual("discover/movie?include_adult=false&include_video=false&page=100&sort_by=popularity.desc", result);
        }
        
        [Test]
        public void GetBrowseQuery_WithSortByRating_ReturnsTrue()
        {
            // Arrange
            criteria[SearchFilterOptions.SortBy] = "Rating";

            // Act
            var result = uut.GetBrowseQuery(criteria);

            // Assert
            Assert.AreEqual("discover/movie?include_adult=false&include_video=false&sort_by=vote_average.desc", result);
        }
        
        [Test]
        public void GetBrowseQuery_WithSortByPopularity_ReturnsTrue()
        {
            // Arrange
            criteria[SearchFilterOptions.SortBy] = "Popularity";

            // Act
            var result = uut.GetBrowseQuery(criteria);

            // Assert
            Assert.AreEqual("discover/movie?include_adult=false&include_video=false&sort_by=popularity.desc", result);
        }
        
        [Test]
        public void GetBrowseQuery_WithInvalidSortBy_ReturnsTrue()
        {
            // Arrange
            criteria[SearchFilterOptions.SortBy] = "Totally Legit Option";

            // Act
            var result = uut.GetBrowseQuery(criteria);

            // Assert
            Assert.AreEqual("discover/movie?include_adult=false&include_video=false&sort_by=popularity.desc", result);
        }
        
        [Test]
        public void GetBrowseQuery_WithPageNumberAndSortBy_ReturnsTrue()
        {
            // Arrange
            criteria[SearchFilterOptions.PageNr] = "64";
            criteria[SearchFilterOptions.SortBy] = "Popularity";

            // Act
            var result = uut.GetBrowseQuery(criteria);

            // Assert
            Assert.AreEqual("discover/movie?include_adult=false&include_video=false&page=64&sort_by=popularity.desc", result);
        }
        
        [Test]
        public void GetSearchQuery_NoSetCriteria_ReturnsTrue()
        {
            // Arrange
            
            // Act
            var result = uut.GetSearchQuery(criteria);

            // Assert
            Assert.AreEqual("search/movie?include_adult=false&include_video=false", result);
        }
        
        [Test]
        public void GetSearchQuery_WithIrrelevantSetCriteria_ReturnsTrue()
        {
            // Arrange
            criteria[SearchFilterOptions.SortBy] = "Rating";
            criteria[SearchFilterOptions.SortOrder] = ".asc";
            
            // Act
            var result = uut.GetSearchQuery(criteria);

            // Assert
            Assert.AreEqual("search/movie?include_adult=false&include_video=false", result);
        }
        
        [Test]
        public void GetSearchQuery_WithText_ReturnsTrue()
        {
            // Arrange
            criteria[SearchFilterOptions.Text] = "Oh, Captain, my captain";
            
            // Act
            var result = uut.GetSearchQuery(criteria);

            // Assert
            Assert.AreEqual("search/movie?include_adult=false&include_video=false&query=Oh,+Captain,+my+captain", result);
        }
        
        [Test]
        public void GetSearchQuery_WithTextAndPageNumber_ReturnsTrue()
        {
            // Arrange
            criteria[SearchFilterOptions.Text] = "Oh, Captain, my captain";
            criteria[SearchFilterOptions.PageNr] = "35";
            
            // Act
            var result = uut.GetSearchQuery(criteria);

            // Assert
            Assert.AreEqual("search/movie?include_adult=false&include_video=false&query=Oh,+Captain,+my+captain&page=35", result);
        }

        [Test]
        public void GetPopularQuery_ReturnsTrue()
        {
            Assert.AreEqual("movie/popular", uut.GetPopularQuery());
        }
        
        [Test]
        public void GetCurrentQuery_ReturnsTrue()
        {
            Assert.AreEqual("movie/now_playing", uut.GetCurrentMovieQuery());
        }
        
        [Test]
        public void GetTopRatedQuery_ReturnsTrue()
        {
            Assert.AreEqual("movie/top_rated", uut.GetTopRatedQuery());
        }
        
        [Test]
        public void GetUpcomingQuery_ReturnsTrue()
        {
            Assert.AreEqual("movie/upcoming", uut.GetUpcomingQuery());
        }
    }
}