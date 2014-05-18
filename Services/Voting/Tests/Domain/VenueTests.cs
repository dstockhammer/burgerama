using System;
using System.Linq;
using Burgerama.Messaging.Events.Voting;
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
            var venue = new Venue(id, string.Empty);

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
            var outing = DateTime.Today.AddDays(-1);
            var votes = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };

            // Act
            var venue = new Venue(id, string.Empty, outing, votes);

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
            var user = Guid.NewGuid().ToString();
            var venue = new Venue(Guid.NewGuid(), string.Empty);
            
            // Act
            var result = venue.AddVote(user);

            // Assert
            Assert.IsTrue(result.Any(e => e is VoteAdded));
            Assert.AreEqual(1, venue.Votes.Count());
            Assert.IsTrue(venue.Votes.Contains(user));
        }

        [TestMethod]
        public void AddVote_ShouldWorkWithMultipleUsers()
        {
            // Arrange
            var user1 = Guid.NewGuid().ToString();
            var user2 = Guid.NewGuid().ToString();
            var venue = new Venue(Guid.NewGuid(), string.Empty);

            // Act
            var result1 = venue.AddVote(user1);
            var result2 = venue.AddVote(user2);

            // Assert
            Assert.IsTrue(result1.Any(e => e is VoteAdded));
            Assert.IsTrue(result2.Any(e => e is VoteAdded));
            Assert.AreEqual(2, venue.Votes.Count());
            Assert.IsTrue(venue.Votes.Contains(user1));
            Assert.IsTrue(venue.Votes.Contains(user2));
        }

        [TestMethod]
        public void AddVote_ShouldOnlyTakeOneVotePerUser()
        {
            // Arrange
            var user1 = Guid.NewGuid().ToString();
            var user2 = Guid.NewGuid().ToString();
            var venue = new Venue(Guid.NewGuid(), string.Empty);
            
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
        public void AddVote_ShouldNotWorkIfVenueHasPreviousOuting()
        {
            // Arrange
            var user = Guid.NewGuid().ToString();
            var venue = new Venue(Guid.NewGuid(), string.Empty, DateTime.Today.AddDays(-1));
            
            // Act
            var result = venue.AddVote(user);

            // Assert
            Assert.IsFalse(result.Any(e => e is VoteAdded));
            Assert.AreEqual(0, venue.Votes.Count());
            Assert.IsFalse(venue.Votes.Contains(user));
        }

        [TestMethod]
        public void AddVote_ShouldNotWorkIfVenueHasFutureOuting()
        {
            // Arrange
            var user = Guid.NewGuid().ToString();
            var venue = new Venue(Guid.NewGuid(), string.Empty, DateTime.Today.AddDays(1));

            // Act
            var result = venue.AddVote(user);

            // Assert
            Assert.IsTrue(result.Any(e => e is VoteAdded));
            Assert.AreEqual(1, venue.Votes.Count());
            Assert.IsTrue(venue.Votes.Contains(user));
        }
    }
}
