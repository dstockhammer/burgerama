using Burgerama.Services.Ratings.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Burgerama.Services.Ratings.Tests.Domain
{
    [TestClass]
    public class RatingTests
    {
        [TestMethod]
        public void Rating_ShouldCreatedCorrectly()
        {
            // Arrange
            var userId = string.Empty;
            const double value = 0.5d;

            // Act
            var rating = new Rating(userId, value);

            // Assert
            Assert.IsNotNull(rating);
            Assert.AreEqual(userId, rating.User);
            Assert.AreEqual(value, rating.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Rating_ShouldNotBeAllowedToBeCreatedWithNegativeValue()
        {
            // Arrange
            var userId = string.Empty;
            const double value = -0.1d;

            // Act
            var rating = new Rating(userId, value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Rating_ShouldNotBeAllowedToBeCreatedWithValueBiggerThanOne()
        {
            // Arrange
            var userId = string.Empty;
            const double value = 1.1d;

            // Act
            var rating = new Rating(userId, value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Rating_ShouldNotBeCreatedWithoutUser()
        {
            var rating = new Rating(null, 0);
        }
    }
}
