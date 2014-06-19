using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Burgerama.Shared.Candidates.Tests.Domain
{
    [TestClass]
    public class CandidateTests
    {
        [TestMethod]
        public void Candidate_ShouldBeCreatedCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var items = Enumerable.Range(1, 3).Select(i => new TestItem());

            // Act
            var candidate = new TestCandidate("test", id, items);

            // Assert
            Assert.IsNotNull(candidate);
            Assert.AreEqual(id, candidate.Reference);
            Assert.AreEqual(3, candidate.Items.Count());
        }

        [TestMethod]
        public void Candidate_ShouldBeCreatedWithNoRepeatedItems()
        {
            // Arrange
            var itemId = Guid.NewGuid();
            var ratings = Enumerable.Range(1, 3).Select(i => new TestItem(itemId));

            // Act
            var candidate = new TestCandidate("test", Guid.NewGuid(), ratings);
            
            // Assert
            Assert.IsNotNull(candidate);
            Assert.AreEqual(1, candidate.Items.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Candidate_ShouldNotBeCreatedWithoutItems()
        {
            var candidate = new TestCandidate("test", Guid.NewGuid(), null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Candidate_ShouldNotBeCreatedWithoutContext()
        {
            var candidate = new TestCandidate(null, Guid.NewGuid());
        }

        [TestMethod]
        public void NoItems_CountShouldBeZero()
        {
            // Arrange
            var candidate = new TestCandidate("test", Guid.NewGuid());

            // Act
            // no items added

            // Assert
            Assert.AreEqual(0, candidate.Items.Count());
        }

        [TestMethod]
        public void CloseDateInTheFuture_CandidateShouldBeActive()
        {
            // Arrange
            var candidate = new TestCandidate("test", Guid.NewGuid());
            candidate.OpenOn(DateTime.Today.AddDays(-1));
            candidate.CloseOn(DateTime.Today.AddDays(1));

            // Act
            var isActive = candidate.IsActiveOn(DateTime.Now);

            // Assert
            Assert.IsTrue(isActive);
        }

        [TestMethod]
        public void CloseDateInThePast_CandidateShouldNotBeActive()
        {
            // Arrange
            var candidate = new TestCandidate("test", Guid.NewGuid());
            candidate.CloseOn(DateTime.Today.AddDays(-1));

            // Act
            var isActive = candidate.IsActiveOn(DateTime.Now);

            // Assert
            Assert.IsFalse(isActive);
        }

        [TestMethod]
        public void NoOpeningDate_CandidateShouldNotBeActive()
        {
            // Arrange
            var candidate = new TestCandidate("test", Guid.NewGuid());

            // Act
            var isActive = candidate.IsActiveOn(DateTime.Now);

            // Assert
            Assert.IsFalse(isActive);
        }

        [TestMethod]
        public void OpeningDateInTheFuture_CandidateShouldNotBeActive()
        {
            // Arrange
            var candidate = new TestCandidate("test", Guid.NewGuid());
            candidate.OpenOn(DateTime.Today.AddDays(1));

            // Act
            var isActive = candidate.IsActiveOn(DateTime.Now);

            // Assert
            Assert.IsFalse(isActive);
        }

        [TestMethod]
        public void OpeningDateInThePast_CandidateShouldNotBeActive()
        {
            // Arrange
            var candidate = new TestCandidate("test", Guid.NewGuid());
            candidate.OpenOn(DateTime.Today.AddDays(-1));

            // Act
            var isActive = candidate.IsActiveOn(DateTime.Now);

            // Assert
            Assert.IsTrue(isActive);
        }
    }
}
