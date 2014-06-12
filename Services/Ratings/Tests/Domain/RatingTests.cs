using Burgerama.Services.Ratings.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Burgerama.Services.Ratings.Tests.Domain
{
    [TestClass]
    public class RatingTests
    {
        [TestMethod]
        public void Rating_ShouldBeCreatedCorrectly()
        {
            // Arrange
            var userId = string.Empty;
            const double value = 0.5d;
            var dateCreated = DateTime.Now;

            // Act
            var rating = new Rating(dateCreated, userId, value, string.Empty);

            // Assert
            Assert.IsNotNull(rating);
            Assert.AreEqual(dateCreated, rating.CreatedOn);
            Assert.AreEqual(userId, rating.UserId);
            Assert.AreEqual(value, rating.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Rating_ShouldNotBeCreatedWithNegativeValue()
        {
            // Arrange
            var userId = string.Empty;
            const double value = -0.1d;
            var dateCreated = DateTime.Now;

            // Act
            var rating = new Rating(dateCreated, userId, value, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Rating_ShouldNotBeCreatedWithValueBiggerThanOne()
        {
            // Arrange
            var userId = string.Empty;
            const double value = 1.1d;
            var dateCreated = DateTime.Now;

            // Act
            var rating = new Rating(dateCreated, userId, value, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Rating_ShouldNotBeCreatedWithoutUser()
        {
            var rating = new Rating(DateTime.Now, null, 0, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Rating_ShouldNotBeCreatedWithoutText()
        {
            var rating = new Rating(DateTime.Now, null, 0, null);
        }
    }
}
