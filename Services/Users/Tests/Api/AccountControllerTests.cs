using System.Threading.Tasks;
using Burgerama.Services.Users.Api.Controllers;
using Burgerama.Services.Users.Api.Models.Account;
using Burgerama.Services.Users.Core;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Burgerama.Services.Users.Tests.Api
{
    [TestClass]
    public class AccountControllerTests
    {
        private AccountController _accountController;

        [TestInitialize]
        public void Initialize()
        {
            var context = new BurgeramaDbContext();
            var userStore = new UserStore<BurgeramaUser>(context);
            var userManager = new BurgeramaUserManager(userStore);

            _accountController = new AccountController(userManager, null);
        }

        [TestMethod]
        public async Task RegisterTest()
        {
            // Arrange
            var model = new RegisterBindingModel
            {
                Email = "foo@bar.com",
                FirstName = "Foo",
                Surname = "Bar",
                Password = "!S3cUr3",
                ConfirmPassword = "!S3cUr3"
            };

            // Act
            var result = await _accountController.Register(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.Inconclusive("This test is for debug purposes only.");
        }
    }
}
