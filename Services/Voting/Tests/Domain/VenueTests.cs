using System;
using System.Linq;
using Burgerama.Services.Voting.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Burgerama.Services.Voting.Tests.Domain
{
    [TestClass]
    public class VenueTests
    {
        [TestMethod]
        public void NewVenue_ShouldBeCreatedCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var venue = new Venue(id);

            // Assert
            Assert.AreEqual(id, venue.Id);
            Assert.AreEqual(0, venue.Votes.Count());
            Assert.IsFalse(venue.LatestOuting.HasValue);
        }

        [TestMethod]
        public void ExistingVenue_ShouldBeCreatedCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var outing = Guid.NewGuid();
            var votes = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

            // Act
            var venue = new Venue(id, outing, votes);

            // Assert
            Assert.AreEqual(id, venue.Id);
            Assert.AreEqual(4, venue.Votes.Count());
            Assert.IsTrue(venue.LatestOuting.HasValue);
            Assert.AreEqual(outing, venue.LatestOuting);
        }

        [TestMethod]
        public void AddVote_ShouldWork()
        {
            // Arrange
            var user = Guid.NewGuid();
            var venue = new Venue(Guid.NewGuid());
            
            // Act
            var result = venue.AddVote(user);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, venue.Votes.Count());
            Assert.IsTrue(venue.Votes.Contains(user));
        }

        [TestMethod]
        public void AddVote_ShouldWorkWithMultipleUsers()
        {
            // Arrange
            var user1 = Guid.NewGuid();
            var user2 = Guid.NewGuid();
            var venue = new Venue(Guid.NewGuid());

            // Act
            var result1 = venue.AddVote(user1);
            var result2 = venue.AddVote(user2);

            // Assert
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
            Assert.AreEqual(2, venue.Votes.Count());
            Assert.IsTrue(venue.Votes.Contains(user1));
            Assert.IsTrue(venue.Votes.Contains(user2));
        }

        [TestMethod]
        public void AddVote_ShouldOnlyTakeOneVotePerUser()
        {
            // Arrange
            var user1 = Guid.NewGuid();
            var user2 = Guid.NewGuid();
            var venue = new Venue(Guid.NewGuid());
            
            // Act
            venue.AddVote(user1);
            venue.AddVote(user1);
            venue.AddVote(user2);
            venue.AddVote(user2);
            venue.AddVote(user1);
            venue.AddVote(user2);

            // Assert
            Assert.AreEqual(2, venue.Votes.Count());
            Assert.IsTrue(venue.Votes.Contains(user1));
            Assert.IsTrue(venue.Votes.Contains(user2));
        }

        [TestMethod]
        public void AddVote_ShouldOnlyWorkIfVenueHasNoOuting()
        {
            // Arrange
            var user = Guid.NewGuid();
            var venue = new Venue(Guid.NewGuid(), Guid.NewGuid());
            
            // Act
            var result = venue.AddVote(user);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(0, venue.Votes.Count());
            Assert.IsFalse(venue.Votes.Contains(user));
        }
    }
}
