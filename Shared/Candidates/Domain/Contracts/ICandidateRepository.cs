using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Shared.Candidates.Domain.Contracts
{
    [ContractClass(typeof(CandidateRepositoryContract))]
    public interface ICandidateRepository
    {
        TCandidate Get<TCandidate, TItem>(string contextKey, Guid reference)
            where TCandidate : Candidate<TItem>
            where TItem : class;

        TCandidate GetPotential<TCandidate, TItem>(string contextKey, Guid reference)
            where TCandidate : PotentialCandidate<TItem>
            where TItem : class;

        IEnumerable<TCandidate> GetAll<TCandidate, TItem>(string contextKey)
            where TCandidate : Candidate<TItem>
            where TItem : class;

        IEnumerable<TCandidate> GetAllPotential<TCandidate, TItem>(string contextKey)
            where TCandidate : PotentialCandidate<TItem>
            where TItem : class;

        void SaveOrUpdate<T>(Candidate<T> candidate)
            where T : class;

        void SaveOrUpdate<T>(PotentialCandidate<T> candidate)
            where T : class;

        void Delete<T>(PotentialCandidate<T> candidate)
            where T : class;
    }
}
