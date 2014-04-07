using System;
using Burgerama.Services.Venues.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Burgerama.Services.Venues.Tests
{
    [TestClass]
    public class VenueTests
    {
        [TestMethod]
        public void AddVote_ShouldIncrementVotes()
        {
            // Arrange
            var user = Guid.NewGuid();
            var venue = new Venue("Test");

            var oldVotes = venue.Votes;

            // Act
            venue.AddVote(user);

            // Assert
            Assert.AreEqual(oldVotes + 1, venue.Votes);
        }

        [TestMethod]
        public void AddVote_ShouldIncrementVotesForEachUser()
        {
            // Arrange
            var user1 = Guid.NewGuid();
            var user2 = Guid.NewGuid();
            var user3 = Guid.NewGuid();
            var venue = new Venue("Test");

            var oldVotes = venue.Votes;

            // Act
            venue.AddVote(user1);
            venue.AddVote(user2);
            venue.AddVote(user3);

            // Assert
            Assert.AreEqual(oldVotes + 3, venue.Votes);
        }

        [TestMethod]
        public void AddVote_ShouldOnlyIncrementOncePerUser()
        {
            // Arrange
            var user = Guid.NewGuid();
            var venue = new Venue("Test");

            var oldVotes = venue.Votes;

            // Act
            venue.AddVote(user);
            venue.AddVote(user);

            // Assert
            Assert.AreEqual(oldVotes + 1, venue.Votes);
        }

        [TestMethod]
        public void AddVote_ShouldNotWorkIfVenueHasOuting()
        {
            // Arrange
            var user = Guid.NewGuid();
            var venue = new Venue("Test");

            // todo: create outing

            var oldVotes = venue.Votes;

            // Act
            venue.AddVote(user);

            // Assert
            Assert.AreEqual(oldVotes + 1, venue.Votes);
        }
    }
}
