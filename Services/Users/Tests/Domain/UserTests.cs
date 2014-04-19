using System;
using Burgerama.Services.Users.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Burgerama.Services.Users.Tests.Domain
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void NewUser_ShouldBeCreatedCorrectly()
        {
            // Arrange
            var userId = "test|" + Guid.NewGuid();
            var email = "test@burgerama.co.uk";

            // Act
            var user = new User(userId, email);

            // Assert
            Assert.IsNotNull(userId);
            Assert.AreEqual(userId, user.Id);
            Assert.AreEqual(email, user.Email);
        }
    }
}
