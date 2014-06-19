using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Shared.Candidates.Domain.Contracts
{
    [ContractClass(typeof(CandidateRepositoryContract))]
    public interface ICandidateRepository
    {
        Candidate<T> Get<T>(string contextKey, Guid reference)
            where T : class;

        PotentialCandidate<T> GetPotential<T>(string contextKey, Guid reference)
            where T : class;

        IEnumerable<Candidate<T>> GetAll<T>(string contextKey)
            where T : class;

        IEnumerable<PotentialCandidate<T>> GetAllPotential<T>(string contextKey)
            where T : class;

        void SaveOrUpdate<T>(Candidate<T> candidate)
            where T : class;

        void SaveOrUpdate<T>(PotentialCandidate<T> candidate)
            where T : class;

        void Delete<T>(PotentialCandidate<T> candidate)
            where T : class;
    }
}
