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

            // Act
            var rating = new Rating(userId, value, string.Empty);

            // Assert
            Assert.IsNotNull(rating);
            Assert.AreEqual(userId, rating.User);
            Assert.AreEqual(value, rating.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Rating_ShouldNotBeCreatedWithNegativeValue()
        {
            // Arrange
            var userId = string.Empty;
            const double value = -0.1d;

            // Act
            var rating = new Rating(userId, value, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Rating_ShouldNotBeCreatedWithValueBiggerThanOne()
        {
            // Arrange
            var userId = string.Empty;
            const double value = 1.1d;

            // Act
            var rating = new Rating(userId, value, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Rating_ShouldNotBeCreatedWithoutUser()
        {
            var rating = new Rating(null, 0, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Rating_ShouldNotBeCreatedWithoutText()
        {
            var rating = new Rating(null, 0, null);
        }
    }
}
