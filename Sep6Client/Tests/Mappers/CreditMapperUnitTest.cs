using System;
using System.Collections.Generic;
using NUnit.Framework;
using Sep6Client.Data.DataHelper.Mappers;
using Sep6Client.Data.DataHelper.Wrappers;

namespace Sep6Client.Tests.Mappers
{
    [TestFixture]
    public class CreditMapperTest
    {
        private CreditListResult wrapper;
        private IList<CrewCreditsResult> crewCreditsList;
        private IList<ActorCreditsResult> actorCreditsList;
        private double avgActorRating;
        private double avgCrewRating;

        [SetUp]
        public void Setup()
        {
            wrapper = new CreditListResult();
            crewCreditsList = new List<CrewCreditsResult>();
            actorCreditsList = new List<ActorCreditsResult>();
            avgActorRating = 0.0;
            avgCrewRating = 0.0;
        }

        [Test]
        public void ToCreditList_EmptyWrapper_ArgumentNullException()
        {
            // Arrange

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => CreditMapper.ToCreditList(wrapper));
        }

        [Test]
        public void ToCreditList_OneCrewMemberNoActors_ReturnsTrue()
        {
            // Arrange
            var crew = new CrewCreditsResult
            {
                MovieId = 1,
                MovieTitle = "Tony The Pony",
                Job = "Producer",
                MovieRating = 2.01,
                Votes = 100,
                MovieReleaseDate = "30-02-2020",
                Department = "Production"
            };
            crewCreditsList.Insert(0, crew);
            wrapper = new CreditListResult
            {
                CrewCredits = crewCreditsList,
                ActorCredits = actorCreditsList
            };

            // Act
            var result = CreditMapper.ToCreditList(wrapper);

            // Assert
            Assert.AreEqual(1, result.CrewCredits.Count);
            Assert.AreEqual(0, result.ActorCredits.Count);
            Assert.AreEqual(1, result.CrewCredits[0].MovieId);
            Assert.AreEqual("Tony The Pony", result.CrewCredits[0].MovieTitle);
            Assert.AreEqual(2.01, result.CrewCredits[0].MovieRating);
            Assert.AreEqual(100, result.CrewCredits[0].Votes);
            Assert.AreEqual("30-02-2020", result.CrewCredits[0].MovieReleaseDate);
            Assert.AreEqual("Producer", result.CrewCredits[0].Job);
            Assert.AreEqual("Production", result.CrewCredits[0].Department);
        }

        [Test]
        public void ToCreditList_OneActorNoCrew_ReturnsTrue()
        {
            wrapper = new CreditListResult
            {
                CrewCredits = crewCreditsList,
                ActorCredits = new List<ActorCreditsResult>
                {
                    new ActorCreditsResult
                    {
                        MovieId = 1,
                        MovieTitle = "Tony The Pony",
                        Character = "Tony",
                        MovieRating = 2.01,
                        Votes = 100,
                        MovieReleaseDate = "30-02-2020"
                    }
                }
            };

            // Act
            var result = CreditMapper.ToCreditList(wrapper);

            // Assert
            Assert.AreEqual(0, result.CrewCredits.Count);
            Assert.AreEqual(1, result.ActorCredits.Count);
            Assert.AreEqual(1, result.ActorCredits[0].MovieId);
            Assert.AreEqual("Tony The Pony", result.ActorCredits[0].MovieTitle);
            Assert.AreEqual(2.01, result.ActorCredits[0].MovieRating);
            Assert.AreEqual(100, result.ActorCredits[0].Votes);
            Assert.AreEqual("30-02-2020", result.ActorCredits[0].MovieReleaseDate);
            Assert.AreEqual("Tony", result.ActorCredits[0].Character);
        }

        [Test]
        public void ToCreditList_OneCrewAndOneActor_ReturnsTrue()
        {
            var crew = new CrewCreditsResult
            {
                MovieId = 1,
                MovieTitle = "Tony The Pony",
                Job = "Producer",
                MovieRating = 2.01,
                Votes = 100,
                MovieReleaseDate = "30-02-2020",
                Department = "Production"
            };
            crewCreditsList.Insert(0, crew);
            wrapper = new CreditListResult
            {
                CrewCredits = crewCreditsList,
                ActorCredits = new List<ActorCreditsResult>
                {
                    new ActorCreditsResult
                    {
                        MovieId = 1,
                        MovieTitle = "Tony The Pony",
                        Character = "Tony",
                        MovieRating = 2.01,
                        Votes = 100,
                        MovieReleaseDate = "30-02-2020"
                    }
                }
            };

            // Act
            var result = CreditMapper.ToCreditList(wrapper);

            // Assert
            Assert.AreEqual(1, result.CrewCredits.Count);
            Assert.AreEqual(1, result.ActorCredits.Count);
            Assert.AreEqual(1, result.ActorCredits[0].MovieId);
            Assert.AreEqual("Tony The Pony", result.ActorCredits[0].MovieTitle);
            Assert.AreEqual(2.01, result.ActorCredits[0].MovieRating);
            Assert.AreEqual(100, result.ActorCredits[0].Votes);
            Assert.AreEqual("30-02-2020", result.ActorCredits[0].MovieReleaseDate);
            Assert.AreEqual("Tony", result.ActorCredits[0].Character);
            Assert.AreEqual(1, result.CrewCredits[0].MovieId);
            Assert.AreEqual("Tony The Pony", result.CrewCredits[0].MovieTitle);
            Assert.AreEqual(2.01, result.CrewCredits[0].MovieRating);
            Assert.AreEqual(100, result.CrewCredits[0].Votes);
            Assert.AreEqual("30-02-2020", result.CrewCredits[0].MovieReleaseDate);
            Assert.AreEqual("Producer", result.CrewCredits[0].Job);
            Assert.AreEqual("Production", result.CrewCredits[0].Department);
        }
    }
}