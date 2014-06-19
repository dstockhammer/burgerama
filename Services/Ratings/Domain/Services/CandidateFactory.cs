using System;
using System.Collections.Generic;
using System.Linq;
using Burgerama.Shared.Candidates.Domain;
using Burgerama.Shared.Candidates.Domain.Contracts;

namespace Burgerama.Services.Ratings.Domain.Services
{
    public sealed class CandidateFactory : ICandidateFactory
    {
        public Candidate<T> Create<T>(string contextKey, Guid reference)
            where T : class
        {
            return new Candidate(contextKey, reference) as Candidate<T>;
        }

        public Candidate<T> Create<T>(string contextKey, Guid reference, IEnumerable<T> items, DateTime? openingDate = null, DateTime? closingDate = null)
            where T : class
        {
            return new Candidate(contextKey, reference, items.Cast<Rating>(), openingDate, closingDate) as Candidate<T>;
        }

        public PotentialCandidate<T> CreatePotential<T>(string contextKey, Guid reference, IEnumerable<T> items)
            where T : class
        {
            return new PotentialCandidate(contextKey, reference, items.Cast<Rating>()) as PotentialCandidate<T>;
        }
    }
}
