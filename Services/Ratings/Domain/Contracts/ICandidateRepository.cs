using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Ratings.Domain.Contracts
{
    [ContractClass(typeof(CandidateRepositoryContract))]
    public interface ICandidateRepository
    {
        Candidate Get(Guid reference, string contextKey);

        IEnumerable<Candidate> GetAll(string contextKey);

        void SaveOrUpdate(Candidate candidate);
    }
}