using Burgerama.Services.Events.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Burgerama.Services.Events.Tests
{
    [TestClass]
    public class EventTests
    {
        /// <summary>
        /// This is just proof of concept...
        /// </summary>
        [TestMethod]
        public void Events_ShouldWork()
        {
            const string title = "This is a test event!";

            var testEvent = new Event(title);

            Assert.AreEqual(title, testEvent.Title);
        }
    }
}
