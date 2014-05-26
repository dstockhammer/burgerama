using System;
using System.Linq;
using Burgerama.Services.Outings.Data.MongoDb;
using Burgerama.Services.Outings.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Burgerama.Services.Outings.Tests.Data
{
    [TestClass]
    public class OutingRepositoryTests
    {
        [TestCleanup]
        public void Cleanup()
        {
            var utils = new MongoDbTestUtils();
            utils.DropOutings();
        }

        [TestMethod]
        public void NewOuting_ShouldBeSavedCorrectly()
        {
            // Arrange
            var outingRepository = new OutingRepository();
            var date = DateTime.Now;
            var venueId = Guid.NewGuid();
            var outing = new Outing(date, venueId);

            // Act
            outingRepository.SaveOrUpdate(outing);

            // Assert
            var loadedOuting = outingRepository.Get(outing.Id);
            Assert.IsNotNull(loadedOuting);
            Assert.AreEqual(outing.Id, loadedOuting.Id);
            Assert.AreEqual(outing.VenueId, loadedOuting.VenueId);
            Assert.IsTrue((outing.Date - loadedOuting.Date).TotalMilliseconds < 1);
        }

        [TestMethod]
        public void ExistingOutings_ShouldBeLoadedCorrectly()
        {
            // Arrange
            var outingRepository = new OutingRepository();

            // Act
            outingRepository.SaveOrUpdate(new Outing(DateTime.Now, Guid.NewGuid()));
            outingRepository.SaveOrUpdate(new Outing(DateTime.Now, Guid.NewGuid()));
            outingRepository.SaveOrUpdate(new Outing(DateTime.Now, Guid.NewGuid()));
            outingRepository.SaveOrUpdate(new Outing(DateTime.Now, Guid.NewGuid()));
            outingRepository.SaveOrUpdate(new Outing(DateTime.Now, Guid.NewGuid()));

            // Assert
            var venues = outingRepository.GetAll();
            Assert.IsNotNull(venues);
            Assert.AreEqual(5, venues.Count());
        }
    }
}
