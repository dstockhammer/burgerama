using Burgerama.Services.Voting.Data.MongoDB;
using Burgerama.Services.Voting.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Burgerama.Services.Voting.Tests.Data
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
            var venue = new Venue(Guid.NewGuid(), string.Empty);

            // Act
            venueRepository.SaveOrUpdate(venue);

            // Assert
            var loadedVenue = venueRepository.Get(venue.Id);
            Assert.IsNotNull(loadedVenue);
            Assert.AreEqual(venue.Id, loadedVenue.Id);
            Assert.AreEqual(venue.LatestOuting.HasValue, loadedVenue.LatestOuting.HasValue);
            Assert.AreEqual(venue.Votes.Count(), loadedVenue.Votes.Count());
        }

        [TestMethod]
        public void ExistingVenue_ShouldBeSavedCorrectly()
        {
            // Arrange
            var venueRepository = new VenueRepository();
            var venue = new Venue(Guid.NewGuid(), string.Empty, DateTime.Today.AddDays(-1), new[]
            {
                Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()
            });

            // Act
            venueRepository.SaveOrUpdate(venue);

            // Assert
            var loadedVenue = venueRepository.Get(venue.Id);
            Assert.IsNotNull(loadedVenue);
            Assert.AreEqual(venue.Id, loadedVenue.Id);
            Assert.AreEqual(venue.LatestOuting, loadedVenue.LatestOuting);
            Assert.AreEqual(venue.Votes.Count(), loadedVenue.Votes.Count());
        }
    }
}
