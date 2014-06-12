using Burgerama.Services.Ratings.Data.Models;
using System;
using System.Linq;
using Burgerama.Services.Ratings.Domain;

namespace Burgerama.Services.Ratings.Data.Converters
{
    internal static class CandidateConverter
    {
        public static CandidateModel ToModel(this Candidate candidate)
        {
            if (candidate == null)
                return null;

            return new CandidateModel
            {
                ContextKey = candidate.ContextKey,
                Reference = candidate.Reference.ToString(),
                OpeningDate = candidate.OpeningDate,
                ClosingDate = candidate.ClosingDate,
                Ratings = candidate.Ratings.Select(r => r.ToModel())
            };
        }
        public static CandidateModel ToModel(this PotentialCandidate candidate)
        {
            if (candidate == null)
                return null;

            return new CandidateModel
            {
                ContextKey = candidate.ContextKey,
                Reference = candidate.Reference.ToString(),
                Ratings = candidate.Ratings.Select(r => r.ToModel())
            };
        }

        public static Candidate ToDomain(this CandidateModel candidate)
        {
            if (candidate == null)
                return null;

            var reference = Guid.Parse(candidate.Reference);
            var ratings = candidate.Ratings.Select(r => r.ToDomain());
            return new Candidate(candidate.ContextKey, reference, ratings, candidate.OpeningDate, candidate.ClosingDate);
        }

        public static PotentialCandidate ToPotential(this CandidateModel candidate)
        {
            if (candidate == null)
                return null;

            var reference = Guid.Parse(candidate.Reference);
            var ratings = candidate.Ratings.Select(r => r.ToDomain());
            return new PotentialCandidate(candidate.ContextKey, reference, ratings);
        }
    }
}
