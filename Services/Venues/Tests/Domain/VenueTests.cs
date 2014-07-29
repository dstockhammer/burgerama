using System;
using System.Linq;
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
            var name = "This is a test.";
            var location = new Location("test", 13.0, 37.0);
            var userId = "testUserId";
            var createdOn = DateTime.Now;

            // Act
            var venue = new Venue(name, location, userId, createdOn);

            // Assert
            Assert.IsNotNull(venue.Id);
        }

        [TestMethod]
        public void DuplicateOutings_ShouldNotBeAccepted()
        {
            // Arrange
            var outing1 = new Outing(Guid.NewGuid(), DateTime.Now);
            var outing2 = new Outing(Guid.NewGuid(), DateTime.Now);
            var location = new Location("test", 13.0, 37.0);
            var venue = new Venue("This is a test.", location, "testUserId", DateTime.Now);

            // Act
            var add1 = venue.AddOuting(outing1);
            var add2 = venue.AddOuting(outing2);
            var add3 = venue.AddOuting(outing1);

            // Assert
            Assert.AreEqual(2, venue.Outings.Count());
            Assert.IsTrue(add1);
            Assert.IsTrue(add2);
            Assert.IsFalse(add3);
        }

        [TestMethod]
        public void ExistingVenue_ShouldBeCreatedCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "This is a test.";
            var location = new Location("test", 13.0, 37.0);
            var userId = "testUserId";
            var createdOn = DateTime.Now;
            var outings = Enumerable.Empty<Outing>();

            // Act
            var venue = new Venue(id, name, location, userId, createdOn, outings);

            // Assert
            Assert.AreEqual(id, venue.Id);
            Assert.AreEqual(name, venue.Name);
            Assert.AreEqual(location, venue.Location);
            Assert.AreEqual(userId, venue.CreatedByUser);
            Assert.AreEqual(createdOn, venue.CreatedOn);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Venue_ShouldNotBeCreatedWithoutLocation()
        {
            // Arrange
            var name = "This is a test.";
            Location location = null;
            var userId = "testUserId";
            var createdOn = DateTime.Now;

            // Act
            var venue = new Venue(name, location, userId, createdOn);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Venue_ShouldNotBeCreatedWithoutUser()
        {
            // Arrange
            var name = "This is a test.";
            var location = new Location("test", 13.0, 37.0);
            string userId = null;
            var createdOn = DateTime.Now;

            // Act
            var venue = new Venue(name, location, userId, createdOn);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Venue_ShouldNotBeCreatedWithoutName()
        {
            // Arrange
            string name = null;
            var location = new Location("test", 13.0, 37.0);
            var userId = "testUserId";
            var createdOn = DateTime.Now;

            // Act
            var venue = new Venue(name, location, userId, createdOn);
        }
    }
}
