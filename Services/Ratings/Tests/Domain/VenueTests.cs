using Burgerama.Services.Ratings.Core;
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
            var venue = new Venue(id, ratings);

            // Assert
            Assert.IsNotNull(venue);
            Assert.AreEqual(id, venue.Id);
            Assert.AreEqual(3, venue.Ratings.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Venue_ShouldBeCreatedWithNoRepeatedRatings()
        {
            // Arrange
            var ratings = Enumerable.Range(1, 3).Select(i => new Rating(string.Empty, i / 10, string.Empty )).ToList();

            // Act
            var venue = new Venue(Guid.NewGuid(), ratings);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Venue_ShouldNotBeCreatedWithoutRatings()
        {
            var venue = new Venue(Guid.NewGuid(), null);
        }

        [TestMethod]
        public void TotalRating_ShouldBeCalculatedCorrectly()
        {
            // Arrange
            var venue = new Venue(Guid.NewGuid());

            // Act
            for(var i = 0; i < 3; i++)
            {
                venue.AddRating(new Rating(i.ToString(), (i + 1.5) / 10, string.Empty));
            }

            // Assert
            Assert.AreEqual(3, venue.Ratings.Count());
            Assert.AreEqual(0.25, venue.TotalRating);
        }
    }
}
