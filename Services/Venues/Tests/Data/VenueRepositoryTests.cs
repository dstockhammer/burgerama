using System;
using System.Linq;
using Burgerama.Services.Venues.Data;
using Burgerama.Services.Venues.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Burgerama.Services.Venues.Tests.Data
{
    [TestClass]
    public class VenueRepositoryTests
    {
        [TestCleanup]
        public void Cleanup()
        {
            var utils = new MongoDbTestUtils();
            utils.DropVenues();
        }

        [TestMethod]
        public void NewVenue_ShouldBeSavedCorrectly()
        {
            // Arrange
            var venueRepository = new VenueRepository();
            var location = new Location("test", 13.0, 37.0);
            var userId = "test|" + Guid.NewGuid();
            var venue = new Venue("This is a test.", location, userId);

            // Act
            venueRepository.SaveOrUpdate(venue);

            // Assert
            var loadedVenue = venueRepository.Get(venue.Id);
            Assert.IsNotNull(loadedVenue);
            Assert.AreEqual(venue.Id, loadedVenue.Id);
            Assert.AreEqual(venue.Title, loadedVenue.Title);
            Assert.AreEqual(venue.Location, loadedVenue.Location);
        }

        [TestMethod]
        public void ExistingVenue_ShouldBeUpdatedCorrectly()
        {
            // Arrange
            var venueRepository = new VenueRepository();
            var location = new Location("test", 13.0, 37.0);
            var userId = "test|" + Guid.NewGuid();
            var venue = new Venue("This is a test.", location, userId);
            venueRepository.SaveOrUpdate(venue);

            // Act
            venue.Url = "http://burgerama.co.uk";
            venue.Description = "This is a test.";
            venueRepository.SaveOrUpdate(venue);

            // Assert
            var loadedVenue = venueRepository.Get(venue.Id);
            Assert.IsNotNull(loadedVenue);
            Assert.AreEqual(venue.Id, loadedVenue.Id);
            Assert.AreEqual(venue.Title, loadedVenue.Title);
            Assert.AreEqual(venue.Location, loadedVenue.Location);
            Assert.AreEqual("http://burgerama.co.uk", loadedVenue.Url);
            Assert.AreEqual("This is a test.", loadedVenue.Description);
        }
        
        [TestMethod]
        public void ExistingVenues_ShouldBeLoadedCorrectly()
        {
            // Arrange
            var venueRepository = new VenueRepository();
            var location = new Location("test", 13.0, 37.0);

            // Act
            venueRepository.SaveOrUpdate(new Venue("This is a test 1.", location, "test|" + Guid.NewGuid()));
            venueRepository.SaveOrUpdate(new Venue("This is a test 2.", location, "test|" + Guid.NewGuid()));
            venueRepository.SaveOrUpdate(new Venue("This is a test 3.", location, "test|" + Guid.NewGuid()));
            venueRepository.SaveOrUpdate(new Venue("This is a test 4.", location, "test|" + Guid.NewGuid()));
            venueRepository.SaveOrUpdate(new Venue("This is a test 5.", location, "test|" + Guid.NewGuid()));

            // Assert
            var venues = venueRepository.GetAll();
            Assert.IsNotNull(venues);
            Assert.AreEqual(5, venues.Count());
        }
    }
}
