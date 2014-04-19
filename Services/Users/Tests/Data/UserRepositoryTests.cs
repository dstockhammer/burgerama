using System;
using Burgerama.Services.Users.Data;
using Burgerama.Services.Users.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Burgerama.Services.Users.Tests.Data
{
    [TestClass]
    public class UserRepositoryTests
    {
        [TestCleanup]
        public void Cleanup()
        {
            var utils = new MongoDbTestUtils();
            //utils.DropUsers();
        }

        [TestMethod]
        public void User_ShouldBeSavedCorrectly()
        {
            // Arrange
            var userRepository = new UserRepository();
            var user = new User(Guid.NewGuid().ToString(), "test@burgerama.co.uk");

            // Act
            userRepository.SaveOrUpdate(user);

            // Assert
            var loadedUser = userRepository.Get(user.Id);
            Assert.IsNotNull(loadedUser);
            Assert.AreEqual(user.Id, loadedUser.Id);
            Assert.AreEqual(user.Email, loadedUser.Email);
        }
    }
}
