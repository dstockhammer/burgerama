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
        private const string ContextKey = "test";
        
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
            var candidate = new Candidate(ContextKey, Guid.NewGuid());

            // Act
            candidateRepository.SaveOrUpdate(candidate);

            // Assert
            var loadedCandidate = candidateRepository.Get(ContextKey, candidate.Reference);
            Assert.IsNotNull(loadedCandidate);
            Assert.AreEqual(candidate.Reference, loadedCandidate.Reference);
            Assert.AreEqual(candidate.Votes.Count(), loadedCandidate.Votes.Count());
            Assert.AreEqual(candidate.OpeningDate, loadedCandidate.OpeningDate);
            Assert.AreEqual(candidate.ClosingDate, loadedCandidate.ClosingDate);
        }
    }
}
