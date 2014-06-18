using Burgerama.Services.Voting.Data.MongoDB.Models;
using Burgerama.Services.Voting.Domain;
using System;
using System.Linq;

namespace Burgerama.Services.Voting.Data.MongoDB.Converters
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
                Votes = candidate.Votes.Select(v => v.ToModel())
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
                Votes = candidate.Votes.Select(v => v.ToModel())
            };
        }

        public static Candidate ToDomain(this CandidateModel candidate)
        {
            if (candidate == null)
                return null;

            var reference = Guid.Parse(candidate.Reference);
            var votes = candidate.Votes.Select(v => v.ToDomain());
            return new Candidate(candidate.ContextKey, reference, votes, candidate.OpeningDate, candidate.ClosingDate);
        }

        public static PotentialCandidate ToPotential(this CandidateModel candidate)
        {
            if (candidate == null)
                return null;

            var reference = Guid.Parse(candidate.Reference);
            var votes = candidate.Votes.Select(v => v.ToDomain());
            return new PotentialCandidate(candidate.ContextKey, reference, votes);
        }
    }
}
