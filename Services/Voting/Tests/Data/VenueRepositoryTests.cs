using System;
using Burgerama.Services.Voting.Data.Repositories;
using Burgerama.Services.Voting.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Burgerama.Services.Voting.Tests.Data
{
    [TestClass]
    public class VenueRepositoryTests
    {
        [TestMethod]
        public void NewVenue_ShouldBeSavedCorrectly()
        {
            var venueRepository = new VenueRepository();

            var venue = new Venue(Guid.NewGuid());

            venueRepository.SaveOrUpdate(venue);
        }

        [TestMethod]
        public void ExistingVenue_ShouldBeSavedCorrectly()
        {
            var venueRepository = new VenueRepository();

            var venue = new Venue(Guid.NewGuid(), Guid.NewGuid(), new[]
            {
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()
            });

            venueRepository.SaveOrUpdate(venue);
        }
    }
}
