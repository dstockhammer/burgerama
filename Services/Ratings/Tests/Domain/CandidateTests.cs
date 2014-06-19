using Burgerama.Services.Ratings.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Burgerama.Services.Ratings.Tests.Domain
{
    [TestClass]
    public class CandidateTests
    {
        [TestMethod]
        public void TotalRating_ShouldDefaultToNull()
        {
            // Arrange
            var venue = new Candidate("test", Guid.NewGuid());

            // Act
            // no ratings added

            // Assert
            Assert.AreEqual(0, venue.Items.Count());
            Assert.AreEqual(null, venue.TotalRating);
        }

        [TestMethod]
        public void CloseDateInTheFuture_RatingShouldBeAdded()
        {
            // Arrange
            var candidate = new Candidate("test", Guid.NewGuid());
            candidate.OpenOn(DateTime.Today.AddDays(-1));
            candidate.CloseOn(DateTime.Today.AddDays(1));

            // Act
            for (var i = 0; i < 3; i++)
            {
                candidate.AddItem(new Rating(DateTime.Now, i.ToString(), i / 10, string.Empty));
            }

            // Assert
            Assert.AreEqual(3, candidate.Items.Count());
        }

        [TestMethod]
        public void CloseDateInThePast_RatingShouldNotBeAdded()
        {
            // Arrange
            var candidate = new Candidate("test", Guid.NewGuid());
            candidate.CloseOn(DateTime.Today.AddDays(-1));

            // Act
            for (var i = 0; i < 3; i++)
            {
                candidate.AddItem(new Rating(DateTime.Now, i.ToString(), i / 10, string.Empty));
            }

            // Assert
            Assert.AreEqual(0, candidate.Items.Count());
        }
        
        [TestMethod]
        public void NoOpeningDate_RatingShouldNotBeAdded()
        {
            // Arrange
            var candidate = new Candidate("test", Guid.NewGuid());

            // Act
            for (var i = 0; i < 3; i++)
            {
                candidate.AddItem(new Rating(DateTime.Now, i.ToString(), i / 10, string.Empty));
            }

            // Assert
            Assert.AreEqual(0, candidate.Items.Count());
        }

        [TestMethod]
        public void OpeningDateInTheFuture_RatingShouldNotBeAdded()
        {
            // Arrange
            var candidate = new Candidate("test", Guid.NewGuid());
            candidate.OpenOn(DateTime.Today.AddDays(1));

            // Act
            for (var i = 0; i < 3; i++)
            {
                candidate.AddItem(new Rating(DateTime.Now, i.ToString(), i / 10, string.Empty));
            }

            // Assert
            Assert.AreEqual(0, candidate.Items.Count());
        }

        [TestMethod]
        public void OpeningDateInThePast_RatingShouldBeAdded()
        {
            // Arrange
            var candidate = new Candidate("test", Guid.NewGuid());
            candidate.OpenOn(DateTime.Today.AddDays(-1));

            // Act
            for (var i = 0; i < 3; i++)
            {
                candidate.AddItem(new Rating(DateTime.Now, i.ToString(), i / 10, string.Empty));
            }

            // Assert
            Assert.AreEqual(3, candidate.Items.Count());
        }
    }
}
