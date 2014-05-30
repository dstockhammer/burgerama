
using System;

namespace Burgerama.Services.Voting.Domain.Contracts
{
    public interface ICandidateRepository
    {
        Candidate Get(Guid reference, string contextKey);

        void SaveOrUpdate(Candidate candidate, string contextKey);
    }
}
