using System;
using Burgerama.Services.Voting.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Burgerama.Services.Voting.Tests.Domain
{
    [TestClass]
    public class VoteTests
    {
        [TestMethod]
        public void Vote_ShouldBeCreatedCorrectly()
        {
            // Arrange
            var userId = string.Empty;
            var dateCreated = DateTime.Now;

            // Act
            var rating = new Vote(dateCreated, userId);

            // Assert
            Assert.IsNotNull(rating);
            Assert.AreEqual(dateCreated, rating.CreatedOn);
            Assert.AreEqual(userId, rating.UserId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Vote_ShouldNotBeCreatedWithoutUser()
        {
            // Arrange
            var dateCreated = DateTime.Now;
            string userId = null;

            // Act
            var vote = new Vote(dateCreated, userId);
        }
    }
}
