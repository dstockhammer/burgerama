using System;
using System.Linq;
using Burgerama.Services.Voting.Data;
using Burgerama.Services.Voting.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var venue = new Venue(Guid.NewGuid());

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
            var venue = new Venue(Guid.NewGuid(), DateTime.Today.AddDays(-1), new[]
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
