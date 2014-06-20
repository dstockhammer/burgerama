using System;
using System.Linq;
using Burgerama.Services.Voting.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Burgerama.Services.Voting.Tests.Domain
{
    [TestClass]
    public class CandidateTests
    {
        [TestMethod]
        public void CloseDateInTheFuture_VoteShouldBeAdded()
        {
            // Arrange
            var candidate = new Candidate("test", Guid.NewGuid());
            candidate.OpenOn(DateTime.Today.AddDays(-1));
            candidate.CloseOn(DateTime.Today.AddDays(1));

            // Act
            for (var i = 0; i < 3; i++)
            {
                candidate.AddItem(new Vote(DateTime.Now, i.ToString()));
            }

            // Assert
            Assert.AreEqual(3, candidate.Items.Count());
        }

        [TestMethod]
        public void CloseDateInThePast_VoteShouldNotBeAdded()
        {
            // Arrange
            var candidate = new Candidate("test", Guid.NewGuid());
            candidate.CloseOn(DateTime.Today.AddDays(-1));

            // Act
            for (var i = 0; i < 3; i++)
            {
                candidate.AddItem(new Vote(DateTime.Now, i.ToString()));
            }

            // Assert
            Assert.AreEqual(0, candidate.Items.Count());
        }

        [TestMethod]
        public void NoOpeningDate_VoteShouldNotBeAdded()
        {
            // Arrange
            var candidate = new Candidate("test", Guid.NewGuid());

            // Act
            for (var i = 0; i < 3; i++)
            {
                candidate.AddItem(new Vote(DateTime.Now, i.ToString()));
            }

            // Assert
            Assert.AreEqual(0, candidate.Items.Count());
        }

        [TestMethod]
        public void OpeningDateInTheFuture_VoteShouldNotBeAdded()
        {
            // Arrange
            var candidate = new Candidate("test", Guid.NewGuid());
            candidate.OpenOn(DateTime.Today.AddDays(1));

            // Act
            for (var i = 0; i < 3; i++)
            {
                candidate.AddItem(new Vote(DateTime.Now, i.ToString()));
            }

            // Assert
            Assert.AreEqual(0, candidate.Items.Count());
        }

        [TestMethod]
        public void OpeningDateInThePast_VoteShouldBeAdded()
        {
            // Arrange
            var candidate = new Candidate("test", Guid.NewGuid());
            candidate.OpenOn(DateTime.Today.AddDays(-1));

            // Act
            for (var i = 0; i < 3; i++)
            {
                candidate.AddItem(new Vote(DateTime.Now, i.ToString()));
            }

            // Assert
            Assert.AreEqual(3, candidate.Items.Count());
        }
    }
}
