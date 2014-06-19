using System;
using Burgerama.Shared.Candidates.Data.MongoDB.Models;
using Burgerama.Shared.Candidates.Domain;
using Burgerama.Shared.Candidates.Domain.Contracts;

namespace Burgerama.Shared.Candidates.Data.MongoDB.Converters
{
    internal static class CandidateConverter
    {
        public static CandidateModel<T> ToModel<T>(this Candidate<T> candidate)
            where T : class
        {
            if (candidate == null)
                return null;

            return new CandidateModel<T>
            {
                ContextKey = candidate.ContextKey,
                Reference = candidate.Reference.ToString(),
                OpeningDate = candidate.OpeningDate,
                ClosingDate = candidate.ClosingDate,
                Items = candidate.Items
            };
        }

        public static CandidateModel<T> ToModel<T>(this PotentialCandidate<T> candidate)
            where T : class
        {
            if (candidate == null)
                return null;

            return new CandidateModel<T>
            {
                ContextKey = candidate.ContextKey,
                Reference = candidate.Reference.ToString(),
                Items = candidate.Items
            };
        }

        public static Candidate<T> ToDomain<T>(this CandidateModel<T> candidate, ICandidateFactory factory)
            where T : class
        {
            if (candidate == null)
                return null;

            var reference = Guid.Parse(candidate.Reference);

            return factory.Create<T>(candidate.ContextKey, reference, candidate.Items, candidate.OpeningDate, candidate.ClosingDate);
        }

        public static PotentialCandidate<T> ToPotential<T>(this CandidateModel<T> candidate, ICandidateFactory factory)
            where T : class
        {
            if (candidate == null)
                return null;

            var reference = Guid.Parse(candidate.Reference);

            return factory.CreatePotential<T>(candidate.ContextKey, reference, candidate.Items);
        }
    }
}
