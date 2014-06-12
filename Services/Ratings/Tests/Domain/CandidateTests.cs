using Burgerama.Services.Ratings.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Burgerama.Services.Ratings.Tests.Domain
{
    [TestClass]
    public class CandidateTests
    {
        [TestMethod]
        public void Candidate_ShouldBeCreatedCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var ratings = Enumerable.Range(1, 3).Select(i => new Rating(DateTime.Now, i.ToString(), i / 10, string.Empty));

            // Act
            var candidate = new Candidate("test", id, ratings);

            // Assert
            Assert.IsNotNull(candidate);
            Assert.AreEqual(id, candidate.Reference);
            Assert.AreEqual(3, candidate.Ratings.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Candidate_ShouldBeCreatedWithNoRepeatedRatings()
        {
            // Arrange
            var ratings = Enumerable.Range(1, 3).Select(i => new Rating(DateTime.Now, string.Empty, i / 10, string.Empty)).ToList();

            // Act
            var candidate = new Candidate("test", Guid.NewGuid(), ratings);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Candidate_ShouldNotBeCreatedWithoutRatings()
        {
            var candidate = new Candidate("test", Guid.NewGuid(), null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Candidate_ShouldNotBeCreatedWithoutContext()
        {
            var candidate = new Candidate(null, Guid.NewGuid());
        }

        [TestMethod]
        public void TotalRating_ShouldBeCalculatedCorrectly()
        {
            // Arrange
            var candidate = new Candidate("test", Guid.NewGuid());
            candidate.OpenOn(DateTime.Today.AddDays(-1));

            // Act
            for(var i = 0; i < 3; i++)
            {
                candidate.AddRating(new Rating(DateTime.Now, i.ToString(), (i + 1.5) / 10, string.Empty));
            }

            // Assert
            Assert.AreEqual(3, candidate.Ratings.Count());
            Assert.AreEqual(0.25, candidate.TotalRating);
        }

        [TestMethod]
        public void TotalRating_ShouldDefaultToNaN()
        {
            // Arrange
            var venue = new Candidate("test", Guid.NewGuid());

            // Act
            // no ratings added

            // Assert
            Assert.AreEqual(0, venue.Ratings.Count());
            Assert.AreEqual(double.NaN, venue.TotalRating);
        }

        [TestMethod]
        public void CloseDateInTheFuture_RatingShouldBeAdded()
        {
            // Arrange
            var candidate = new Candidate("test", Guid.NewGuid());
            candidate.OpenOn(DateTime.Today.AddDays(-1));
            candidate.CloseOn(DateTime.Today.AddDays(1));

            // Act
            for (var i = 0; i < 3; i++)
            {
                candidate.AddRating(new Rating(DateTime.Now, i.ToString(), i / 10, string.Empty));
            }

            // Assert
            Assert.AreEqual(3, candidate.Ratings.Count());
        }

        [TestMethod]
        public void CloseDateInThePast_RatingShouldNotBeAdded()
        {
            // Arrange
            var candidate = new Candidate("test", Guid.NewGuid());
            candidate.CloseOn(DateTime.Today.AddDays(-1));

            // Act
            for (var i = 0; i < 3; i++)
            {
                candidate.AddRating(new Rating(DateTime.Now, i.ToString(), i / 10, string.Empty));
            }

            // Assert
            Assert.AreEqual(0, candidate.Ratings.Count());
        }
        
        [TestMethod]
        public void NoOpeningDate_RatingShouldNotBeAdded()
        {
            // Arrange
            var candidate = new Candidate("test", Guid.NewGuid());

            // Act
            for (var i = 0; i < 3; i++)
            {
                candidate.AddRating(new Rating(DateTime.Now, i.ToString(), i / 10, string.Empty));
            }

            // Assert
            Assert.AreEqual(0, candidate.Ratings.Count());
        }

        [TestMethod]
        public void OpeningDateInTheFuture_RatingShouldNotBeAdded()
        {
            // Arrange
            var candidate = new Candidate("test", Guid.NewGuid());
            candidate.OpenOn(DateTime.Today.AddDays(1));

            // Act
            for (var i = 0; i < 3; i++)
            {
                candidate.AddRating(new Rating(DateTime.Now, i.ToString(), i / 10, string.Empty));
            }

            // Assert
            Assert.AreEqual(0, candidate.Ratings.Count());
        }

        [TestMethod]
        public void OpeningDateInThePast_RatingShouldBeAdded()
        {
            // Arrange
            var candidate = new Candidate("test", Guid.NewGuid());
            candidate.OpenOn(DateTime.Today.AddDays(-1));

            // Act
            for (var i = 0; i < 3; i++)
            {
                candidate.AddRating(new Rating(DateTime.Now, i.ToString(), i / 10, string.Empty));
            }

            // Assert
            Assert.AreEqual(3, candidate.Ratings.Count());
        }
    }
}
