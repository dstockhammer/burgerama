using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Shared.Candidates.Domain.Contracts
{
    [ContractClass(typeof(CandidateFactoryContract))]
    public interface ICandidateFactory
    {
        Candidate<T> Create<T>(string contextKey, Guid reference)
            where T : class;

        Candidate<T> Create<T>(string contextKey, Guid reference, IEnumerable<T> items, DateTime? openingDate = null, DateTime? closingDate = null)
            where T : class;

        PotentialCandidate<T> CreatePotential<T>(string contextKey, Guid reference, IEnumerable<T> items)
            where T : class;
    }
}
