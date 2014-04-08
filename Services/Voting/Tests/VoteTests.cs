using System;
using Burgerama.Services.Voting.Core.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Burgerama.Services.Voting.Tests
{
    [TestClass]
    public class VoteTests
    {
        /// <summary>
        /// This is just proof of concept...
        /// </summary>
        [TestMethod]
        public void Vote_ShouldWork()
        {
            var user = Guid.NewGuid();
            var venue = Guid.NewGuid();

            var vote = new Vote(user, venue);

            Assert.AreEqual(user, vote.User);
            Assert.AreEqual(venue, vote.Venue);
        }
    }
}
