
using System;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Voting.Domain.Contracts
{
    [ContractClass(typeof(CandidateRepositoryContract))]
    public interface ICandidateRepository
    {
        Candidate Get(Guid reference, string contextKey);

        void SaveOrUpdate(Candidate candidate, string contextKey);
    }
}
