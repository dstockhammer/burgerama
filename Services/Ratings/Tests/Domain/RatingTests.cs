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
            var dateCreated = DateTime.Now;
            var userId = string.Empty;
            var value = 0.5d;
            var text = string.Empty;

            // Act
            var rating = new Rating(dateCreated, userId, value, text);

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
            var dateCreated = DateTime.Now;
            var userId = string.Empty;
            var value = -0.1d;
            var text = string.Empty;

            // Act
            var rating = new Rating(dateCreated, userId, value, text);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Rating_ShouldNotBeCreatedWithValueBiggerThanOne()
        {
            // Arrange
            var dateCreated = DateTime.Now;
            var userId = string.Empty;
            var value = 1.1d;
            var text = string.Empty;

            // Act
            var rating = new Rating(dateCreated, userId, value, text);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Rating_ShouldNotBeCreatedWithoutUser()
        {
            // Arrange
            var dateCreated = DateTime.Now;
            string userId = null;
            var value = 0;
            var text = string.Empty;

            // Act
            var rating = new Rating(dateCreated, userId, value, text);
        }
    }
}
