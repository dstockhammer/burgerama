using Burgerama.Services.Ratings.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Burgerama.Services.Ratings.Tests.Domain
{
    [TestClass]
    public class VenueTests
    {
        [TestMethod]
        public void Venue_ShouldBeCreatedCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var ratings = Enumerable.Range(1, 3).Select(i => new Rating(i.ToString(), i / 10, string.Empty));

            // Act
            var venue = new Candidate(id, "test venue", ratings);

            // Assert
            Assert.IsNotNull(venue);
            Assert.AreEqual(id, venue.Reference);
            Assert.AreEqual(3, venue.Ratings.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Venue_ShouldBeCreatedWithNoRepeatedRatings()
        {
            // Arrange
            var ratings = Enumerable.Range(1, 3).Select(i => new Rating(string.Empty, i / 10, string.Empty )).ToList();

            // Act
            var venue = new Candidate(Guid.NewGuid(), "test venue", ratings);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Venue_ShouldNotBeCreatedWithoutRatings()
        {
            var venue = new Candidate(Guid.NewGuid(), null);
        }

        [TestMethod]
        public void TotalRating_ShouldBeCalculatedCorrectly()
        {
            // Arrange
            var venue = new Candidate(Guid.NewGuid(), "test venue");

            // Act
            for(var i = 0; i < 3; i++)
            {
                venue.AddRating(new Rating(i.ToString(), (i + 1.5) / 10, string.Empty));
            }

            // Assert
            Assert.AreEqual(3, venue.Ratings.Count());
            Assert.AreEqual(0.25, venue.TotalRating);
        }

        [TestMethod]
        public void TotalRating_ShouldDefaultToNaN()
        {
            // Arrange
            var venue = new Candidate(Guid.NewGuid(), "test venue");

            // Act
            // no ratings added

            // Assert
            Assert.AreEqual(0, venue.Ratings.Count());
            Assert.AreEqual(double.NaN, venue.TotalRating);
        }

        [TestMethod]
        public void VenueWithFutureOuting_RatingShouldBeAdded()
        {
            // Arrange
            var venue = new Candidate(Guid.NewGuid(), "test venue");
            venue.AddOuting(DateTime.Today.AddDays(1));

            // Act
            for (var i = 0; i < 3; i++)
            {
                venue.AddRating(new Rating(i.ToString(), i  / 10, string.Empty));
            }

            // Assert
            Assert.AreEqual(3, venue.Ratings.Count());
        }

        [TestMethod]
        public void VenueWithPastOuting_RatingShouldNotBeAdded()
        {
            // Arrange
            var venue = new Candidate(Guid.NewGuid(), "test venue");
            venue.AddOuting(DateTime.Today.AddDays(-1));

            // Act
            for (var i = 0; i < 3; i++)
            {
                venue.AddRating(new Rating(i.ToString(), i  / 10, string.Empty));
            }

            // Assert
            Assert.AreEqual(0, venue.Ratings.Count());
        }
    }
}
