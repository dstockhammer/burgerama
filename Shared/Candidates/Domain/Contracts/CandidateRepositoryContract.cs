using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Shared.Candidates.Domain.Contracts
{
    [ContractClassFor(typeof(ICandidateRepository))]
    internal abstract class CandidateRepositoryContract : ICandidateRepository
    {
        public Candidate<T> Get<T>(string contextKey, Guid reference)
            where T : class
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);

            return default(Candidate<T>);
        }

        public PotentialCandidate<T> GetPotential<T>(string contextKey, Guid reference)
            where T : class
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);

            return default(PotentialCandidate<T>);
        }

        public IEnumerable<Candidate<T>> GetAll<T>(string contextKey)
            where T : class
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);
            Contract.Ensures(Contract.Result<IEnumerable<Candidate<T>>>() != null);

            return default(IEnumerable<Candidate<T>>);
        }

        public IEnumerable<PotentialCandidate<T>> GetAllPotential<T>(string contextKey)
            where T : class
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);
            Contract.Ensures(Contract.Result<IEnumerable<PotentialCandidate<T>>>() != null);

            return default(IEnumerable<PotentialCandidate<T>>);
        }

        public void SaveOrUpdate<T>(Candidate<T> candidate)
            where T : class
        {
            Contract.Requires<ArgumentNullException>(candidate != null);
        }

        public void SaveOrUpdate<T>(PotentialCandidate<T> candidate)
            where T : class
        {
            Contract.Requires<ArgumentNullException>(candidate != null);
        }

        public void Delete<T>(PotentialCandidate<T> candidate)
            where T : class
        {
            Contract.Requires<ArgumentNullException>(candidate != null);
        }
    }
}
