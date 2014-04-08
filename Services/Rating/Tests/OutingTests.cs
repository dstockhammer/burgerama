using Burgerama.Services.Rating.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Burgerama.Services.Rating.Tests
{
    [TestClass]
    public class OutingTests
    {
        /// <summary>
        /// This is just proof of concept...
        /// </summary>
        [TestMethod]
        public void Outings_ShouldWork()
        {
            const string title = "This is a test outing!";

            var outing = new Outing(title);

            Assert.AreEqual(title, outing.Title);
        }
    }
}
