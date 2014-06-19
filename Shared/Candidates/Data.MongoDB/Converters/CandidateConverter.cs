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

        public static TCandidate ToDomain<TCandidate, TItem>(this CandidateModel<TItem> candidate, ICandidateFactory factory)
            where TCandidate : Candidate<TItem>
            where TItem : class
        {
            if (candidate == null)
                return null;

            var reference = Guid.Parse(candidate.Reference);

            return (TCandidate)factory.Create(candidate.ContextKey, reference, candidate.Items, candidate.OpeningDate, candidate.ClosingDate);
        }

        public static TCandidate ToPotential<TCandidate, TItem>(this CandidateModel<TItem> candidate, ICandidateFactory factory)
            where TCandidate : PotentialCandidate<TItem>
            where TItem : class
        {
            if (candidate == null)
                return null;

            var reference = Guid.Parse(candidate.Reference);

            return (TCandidate)factory.CreatePotential(candidate.ContextKey, reference, candidate.Items);
        }
    }
}
