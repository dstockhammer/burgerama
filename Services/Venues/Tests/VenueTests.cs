using Burgerama.Services.Venues.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Burgerama.Services.Venues.Tests
{
    [TestClass]
    public class VenueTests
    {
        /// <summary>
        /// This is just proof of concept...
        /// </summary>
        [TestMethod]
        public void Venues_ShouldWork()
        {
            const string title = "This is a test venue!";

            var venue = new Venue(title);

            Assert.AreEqual(title, venue.Title);
        }
    }
}
