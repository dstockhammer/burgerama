using System;
using Burgerama.Services.Venues.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Burgerama.Services.Venues.Tests.Domain
{
    [TestClass]
    public class VenueTests
    {
        [TestMethod]
        public void NewVenue_ShouldBeCreatedCorrectly()
        {
            // Arrange
            const string title = "This is a test.";
            var location = new Location("test", 13.0, 37.0);
            const string userId = "testUserId";

            // Act
            var venue = new Venue(title, location, userId);

            // Assert
            Assert.IsNotNull(venue.Id);
        }

        [TestMethod]
        public void ExistingVenue_ShouldBeCreatedCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            const string title = "This is a test.";
            var location = new Location("test", 13.0, 37.0);
            const string userId = "testUserId";
            var createdOn = DateTime.Now;

            // Act
            var venue = new Venue(id, title, location, userId, createdOn);

            // Assert
            Assert.AreEqual(id, venue.Id);
            Assert.AreEqual(title, venue.Name);
            Assert.AreEqual(location, venue.Location);
            Assert.AreEqual(userId, venue.CreatedByUser);
            Assert.AreEqual(createdOn, venue.CreatedOn);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Venue_ShouldNotBeCreatedWithoutLocation()
        {
            var venue = new Venue("This is a test.", null, "testUserId");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Venue_ShouldNotBeCreatedWithoutUser()
        {
            // Arrange
            const string title = "This is a test.";
            var location = new Location("test", 13.0, 37.0);

            // Act
            var venue = new Venue(title, location, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Venue_ShouldNotBeCreatedWithoutTitle()
        {
            // Arrange
            var location = new Location("test", 13.0, 37.0);

            // Act
            var venue = new Venue(null, location, "testUserId");
        }
    }
}
