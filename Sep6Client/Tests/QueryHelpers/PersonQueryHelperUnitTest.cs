using System.Collections.Generic;
using NUnit.Framework;
using Sep6Client.Data.DataHelper.Search;

namespace Tests.QueryHelpers
{
    [TestFixture]
    public class PersonQueryHelperUnitTest
    {
        private PersonQueryHelper uut;
        private Dictionary<SearchFilterOptions, string> criteria;

        [SetUp]
        public void Setup()
        {
            uut = new PersonQueryHelper();
            criteria = new Dictionary<SearchFilterOptions, string>
            {
                {SearchFilterOptions.Text, ""},
                {SearchFilterOptions.PageNr, ""},
                {SearchFilterOptions.SortBy, ""},
                {SearchFilterOptions.SortOrder, ""}
            };
        }
        
        [Test]
        public void GetSearchQuery_WithNoSetCriteria_ReturnsTrue()
        {
            // Arrange
            
            // Act
            var result = uut.GetSearchQuery(criteria);
            
            // Assert
            Assert.AreEqual("search/person?include_adult=false", result);
        }
        
        [Test]
        public void GetSearchQuery_WithIrrelevantSetCriteria_ReturnsTrue()
        {
            // Arrange
            criteria[SearchFilterOptions.SortOrder] = "Not";
            criteria[SearchFilterOptions.SortBy] = "Here";
            
            // Act
            var result = uut.GetSearchQuery(criteria);
            
            // Assert
            Assert.AreEqual("search/person?include_adult=false", result);
        }
        
        [Test]
        public void GetSearchQuery_WithOnlyText_ReturnsTrue()
        {
            // Arrange
            criteria[SearchFilterOptions.Text] = "Jack Rabbit";
            
            // Act
            var result = uut.GetSearchQuery(criteria);
            
            // Assert
            Assert.AreEqual("search/person?include_adult=false&query=Jack+Rabbit", result);
        }

        [Test]
        public void GetSearchQuery_WithTextAndPageNumber_ReturnsTrue()
        {
            // Arrange
            criteria[SearchFilterOptions.PageNr] = "4";
            criteria[SearchFilterOptions.Text] = "Jack Rabbit";
            
            // Act
            var result = uut.GetSearchQuery(criteria);
            
            // Assert
            Assert.AreEqual("search/person?include_adult=false&query=Jack+Rabbit&page=4", result);
        }
        
        [Test]
        public void GetBrowseQuery_WithPageNumber_ReturnsTrue()
        {
            // Arrange
            criteria[SearchFilterOptions.PageNr] = "4";
            
            // Act
            var result = uut.GetBrowseQuery(criteria);
            
            // Assert
            Assert.AreEqual("person/popular?page=4", result);
        }
        
        [Test]
        public void GetBrowseQuery_WithNoSetCriteria_ReturnsTrue()
        {
            // Arrange
            
            // Act
            var result = uut.GetBrowseQuery(criteria);
            
            // Assert
            Assert.AreEqual("person/popular?page=", result);
        }
        
        [Test]
        public void GetBrowseQuery_WithIrrelevantSetCriteria_ReturnsTrue()
        {
            // Arrange
            criteria[SearchFilterOptions.Text] = "Not";
            criteria[SearchFilterOptions.SortOrder] = "Here";
            criteria[SearchFilterOptions.SortBy] = ".";
            
            // Act
            var result = uut.GetBrowseQuery(criteria);
            
            // Assert
            Assert.AreEqual("person/popular?page=", result);
        }
    }
}