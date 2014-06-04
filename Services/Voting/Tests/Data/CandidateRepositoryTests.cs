using Burgerama.Services.Voting.Data.MongoDB;
using Burgerama.Services.Voting.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Burgerama.Services.Voting.Tests.Data
{
    [TestClass]
    public class CandidateRepositoryTests
    {
        const string ContextKey = "venues";
        
        [TestCleanup]
        public void Cleanup()
        {
            var utils = new MongoDbTestUtils();
            utils.DropCandidates();
        }

        [TestMethod]
        public void NewCandidate_ShouldBeSavedCorrectly()
        {
            // Arrange
            var candidateRepository = new CandidateRepository();
            var candidate = new Candidate(Guid.NewGuid());

            // Act
            candidateRepository.SaveOrUpdate(candidate, ContextKey);

            // Assert
            var loadedCandidate = candidateRepository.Get(candidate.Reference, ContextKey);
            Assert.IsNotNull(loadedCandidate);
            Assert.AreEqual(candidate.Reference, loadedCandidate.Reference);
            Assert.AreEqual(candidate.Votes.Count(), loadedCandidate.Votes.Count());
            Assert.AreEqual(candidate.Expiry, loadedCandidate.Expiry);
        }
    }
}
