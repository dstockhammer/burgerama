using System;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Voting.Domain.Contracts
{
    internal abstract class CandidateRepositoryContract : ICandidateRepository
    {
        public Candidate Get(Guid reference, string contextKey)
        {
            Contract.Ensures(Contract.Result<Candidate>() != null);

            return default(Candidate);
        }

        public void SaveOrUpdate(Candidate candidate, string contextKey)
        {
            Contract.Requires<ArgumentNullException>(candidate != null);
        }
    }
}
