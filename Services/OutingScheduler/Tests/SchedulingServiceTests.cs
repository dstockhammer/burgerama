using System;
using System.Collections.Generic;
using Burgerama.Services.OutingScheduler.Domain;
using Burgerama.Services.OutingScheduler.Domain.Contracts;
using Burgerama.Services.OutingScheduler.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Burgerama.Services.OutingScheduler.Tests
{
    [TestClass]
    public class SchedulingServiceTests
    {
        [TestMethod]
        public void NoPotentialOutings_ShouldNotScheduleOuting()
        {
            // Arrange
            var date = DateTime.Today.AddDays(1).AddHours(19);

            var outing1 = Guid.NewGuid();
            var outing2 = Guid.NewGuid();
            var outing3 = Guid.NewGuid();

            var venue1 = Guid.NewGuid();
            var venue2 = Guid.NewGuid();
            var venue3 = Guid.NewGuid();
            
            var outings = new[]
            {
                new Outing(outing1, DateTime.Now, venue1),
                new Outing(outing2, DateTime.Now, venue2),
                new Outing(outing3, DateTime.Now, venue3)
            };
            var venues = new[]
            {
                new Venue(venue1, "No title", 3),
                new Venue(venue2, "No title", 4),
                new Venue(venue3, "No title", 5)
            };

            var service = SetupService(outings, venues);

            // Act
            var outing = service.ScheduleOuting(date);

            // Assert
            Assert.IsNull(outing);
        }

        [TestMethod]
        public void PossibleOutingsWithDifferentVotes_ShouldScheduleCorrectOuting()
        {
            // Arrange
            var date = DateTime.Today.AddDays(1).AddHours(19);

            var outing1 = Guid.NewGuid();
            var outing2 = Guid.NewGuid();
            var outing3 = Guid.NewGuid();

            var venue1 = Guid.NewGuid();
            var venue2 = Guid.NewGuid();
            var venue3 = Guid.NewGuid();

            var outings = new[]
            {
                new Outing(outing1, DateTime.Now, Guid.NewGuid()),
                new Outing(outing2, DateTime.Now, Guid.NewGuid()),
                new Outing(outing3, DateTime.Now, Guid.NewGuid())
            };
            var venues = new[]
            {
                new Venue(venue1, "No title", 3),
                new Venue(venue2, "No title", 4),
                new Venue(venue3, "No title", 5)
            };

            var service = SetupService(outings, venues);

            // Act
            var outing = service.ScheduleOuting(date);

            // Assert
            Assert.AreEqual(venue3, outing.Venue.Id);
        }

        [TestMethod]
        public void VenueWithHighestVoteAlreadyHasOuting_ShouldScheduleNextBestOuting()
        {
            // Arrange
            var date = DateTime.Today.AddDays(1).AddHours(19);

            var outing1 = Guid.NewGuid();
            var outing2 = Guid.NewGuid();
            var outing3 = Guid.NewGuid();

            var venue1 = Guid.NewGuid();
            var venue2 = Guid.NewGuid();
            var venue3 = Guid.NewGuid();

            var outings = new[]
            {
                new Outing(outing1, DateTime.Now, Guid.NewGuid()),
                new Outing(outing2, DateTime.Now, Guid.NewGuid()),
                new Outing(outing3, DateTime.Now, venue3)
            };
            var venues = new[]
            {
                new Venue(venue1, "No title", 3),
                new Venue(venue2, "No title", 4),
                new Venue(venue3, "No title", 5)
            };

            var service = SetupService(outings, venues);

            // Act
            var outing = service.ScheduleOuting(date);

            // Assert
            Assert.AreEqual(venue2, outing.Venue.Id);
        }

        [TestMethod]
        public void OutingsWithSameVotes_ShouldScheduleAnyOuting()
        {
            // Arrange
            var date = DateTime.Today.AddDays(1).AddHours(19);

            var outing1 = Guid.NewGuid();
            var outing2 = Guid.NewGuid();
            var outing3 = Guid.NewGuid();

            var venue1 = Guid.NewGuid();
            var venue2 = Guid.NewGuid();
            var venue3 = Guid.NewGuid();

            var outings = new[]
            {
                new Outing(outing1, DateTime.Now, Guid.NewGuid()),
                new Outing(outing2, DateTime.Now, Guid.NewGuid()),
                new Outing(outing3, DateTime.Now, Guid.NewGuid())
            };
            var venues = new[]
            {
                new Venue(venue1, "No title", 3),
                new Venue(venue2, "No title", 5),
                new Venue(venue3, "No title", 5)
            };

            var service = SetupService(outings, venues);

            // Act
            var outing = service.ScheduleOuting(date);

            // Assert
            Assert.IsTrue(outing.Venue.Id == venue2 || outing.Venue.Id == venue3);
        }

        private SchedulingService SetupService(IEnumerable<Outing> outings, IEnumerable<Venue> venues)
        {
            var outingRepositoryMock = new Mock<IOutingRepository>();
            outingRepositoryMock.Setup(m => m.GetAll()).Returns(outings);

            var venueRepositoryMock = new Mock<IVenueRepository>();
            venueRepositoryMock.Setup(m => m.GetAll()).Returns(venues);

            return new SchedulingService(outingRepositoryMock.Object, venueRepositoryMock.Object);
        }
    }
}
