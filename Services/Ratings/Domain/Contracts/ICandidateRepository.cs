using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Ratings.Domain.Contracts
{
    [ContractClass(typeof(CandidateRepositoryContract))]
    public interface ICandidateRepository
    {
        Candidate Get(string contextKey, Guid reference);

        PotentialCandidate GetPotential(string contextKey, Guid reference);

        IEnumerable<Candidate> GetAll(string contextKey);

        IEnumerable<PotentialCandidate> GetAllPotential(string contextKey);

        void SaveOrUpdate(Candidate candidate);

        void SaveOrUpdate(PotentialCandidate candidate);

        void Delete(PotentialCandidate candidate);
    }
}
