using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Voting.Domain.Contracts
{
    [ContractClassFor(typeof(ICandidateRepository))]
    internal abstract class CandidateRepositoryContract : ICandidateRepository
    {
        public Candidate Get(string contextKey, Guid reference)
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);

            return default(Candidate);
        }

        public PotentialCandidate GetPotential(string contextKey, Guid reference)
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);

            return default(PotentialCandidate);
        }

        public IEnumerable<Candidate> GetAll(string contextKey)
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);
            Contract.Ensures(Contract.Result<IEnumerable<Candidate>>() != null);

            return default(IEnumerable<Candidate>);
        }

        public IEnumerable<PotentialCandidate> GetAllPotential(string contextKey)
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);
            Contract.Ensures(Contract.Result<IEnumerable<PotentialCandidate>>() != null);

            return default(IEnumerable<PotentialCandidate>);
        }

        public void SaveOrUpdate(Candidate candidate)
        {
            Contract.Requires<ArgumentNullException>(candidate != null);
        }

        public void SaveOrUpdate(PotentialCandidate candidate)
        {
            Contract.Requires<ArgumentNullException>(candidate != null);
        }

        public void Delete(PotentialCandidate candidate)
        {
            Contract.Requires<ArgumentNullException>(candidate != null);
        }
    }
}
