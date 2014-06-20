using System.Linq;
using Burgerama.Services.Voting.Api.Models;
using Burgerama.Services.Voting.Domain;

namespace Burgerama.Services.Voting.Api.Converters
{
    internal static class CandidateConverter
    {
        public static CandidateModel ToModel(this Candidate candidate, string userId)
        {
            if (candidate == null)
                return null;

            return new CandidateModel
            {
                ContextKey = candidate.ContextKey,
                Reference = candidate.Reference,
                IsValidated = true,
                OpeningDate = candidate.OpeningDate,
                ClosingDate = candidate.ClosingDate,
                VotesCount = candidate.Items.Count(),
                CanUserVote = candidate.CanUserVote(userId),
                UserVote = candidate.Items.SingleOrDefault(v => v.UserId == userId).ToModel()
            };
        }

        public static CandidateModel ToModel(this PotentialCandidate candidate, string userId)
        {
            if (candidate == null)
                return null;

            return new CandidateModel
            {
                ContextKey = candidate.ContextKey,
                Reference = candidate.Reference,
                IsValidated = false,
                OpeningDate = null,
                ClosingDate = null,
                VotesCount = candidate.Items.Count(),
                CanUserVote = candidate.Items.Any(v => v.UserId == userId),
                UserVote = candidate.Items.SingleOrDefault(v => v.UserId == userId).ToModel()
            };
        }
    }
}
