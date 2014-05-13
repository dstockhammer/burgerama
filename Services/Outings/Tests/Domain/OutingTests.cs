using System;
using Burgerama.Services.Outings.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Burgerama.Services.Outings.Tests.Domain
{
    [TestClass]
    public class OutingTests
    {
        [TestMethod]
        public void NewOuting_ShouldBeCreatedCorrectly()
        {
            // Arrange
            var date = DateTime.Now;
            var venue = Guid.NewGuid();

            // Act
            var outing = new Outing(date, venue);

            // Assert
            Assert.AreEqual(date, outing.Date);
            Assert.AreEqual(venue, outing.VenueId);
        }

        [TestMethod]
        public void ExistingOuting_ShouldBeCreatedCorrectly()
        {
            // Arrange
            var outingId = Guid.NewGuid();
            var date = DateTime.Now;
            var venue = Guid.NewGuid();

            // Act
            var outing = new Outing(outingId, date, venue);

            // Assert
            Assert.AreEqual(outingId, outing.Id);
            Assert.AreEqual(date, outing.Date);
            Assert.AreEqual(venue, outing.VenueId);
        }
    }
}
