using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Shared.Candidates.Domain.Contracts
{
    [ContractClassFor(typeof(ICandidateRepository))]
    internal abstract class CandidateRepositoryContract : ICandidateRepository
    {
        public TCandidate Get<TCandidate, TItem>(string contextKey, Guid reference)
            where TCandidate : Candidate<TItem>
            where TItem : class
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);

            return default(TCandidate);
        }

        public TCandidate GetPotential<TCandidate, TItem>(string contextKey, Guid reference)
            where TCandidate : PotentialCandidate<TItem>
            where TItem : class
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);

            return default(TCandidate);
        }

        public IEnumerable<TCandidate> GetAll<TCandidate, TItem>(string contextKey)
            where TCandidate : Candidate<TItem>
            where TItem : class
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);
            Contract.Ensures(Contract.Result<IEnumerable<TCandidate>>() != null);

            return default(IEnumerable<TCandidate>);
        }

        public IEnumerable<TCandidate> GetAllPotential<TCandidate, TItem>(string contextKey)
            where TCandidate : PotentialCandidate<TItem>
            where TItem : class
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);
            Contract.Ensures(Contract.Result<IEnumerable<TCandidate>>() != null);

            return default(IEnumerable<TCandidate>);
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
