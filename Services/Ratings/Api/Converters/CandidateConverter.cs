using System.Linq;
using Burgerama.Services.Ratings.Api.Models;
using Burgerama.Services.Ratings.Domain;

namespace Burgerama.Services.Ratings.Api.Converters
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
                RatingsCount = candidate.Items.Count(),
                TotalRating = candidate.TotalRating,
                CanUserRate = candidate.CanUserRate(userId),
                UserRating = candidate.Items.SingleOrDefault(r => r.UserId == userId).ToModel()
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
                RatingsCount = candidate.Items.Count(),
                TotalRating = candidate.TotalRating,
                CanUserRate = candidate.Items.Any(r => r.UserId == userId),
                UserRating = candidate.Items.SingleOrDefault(r => r.UserId == userId).ToModel()
            };
        }
    }
}
