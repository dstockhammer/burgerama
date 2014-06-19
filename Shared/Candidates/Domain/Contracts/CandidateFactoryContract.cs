using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Shared.Candidates.Domain.Contracts
{
    [ContractClassFor(typeof(ICandidateFactory))]
    internal abstract class CandidateFactoryContract : ICandidateFactory
    {
        public Candidate<T> Create<T>(string contextKey, Guid reference)
            where T : class
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);

            return default(Candidate<T>);
        }

        public Candidate<T> Create<T>(string contextKey, Guid reference, IEnumerable<T> items, DateTime? openingDate = null, DateTime? closingDate = null)
            where T : class
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);
            Contract.Requires<ArgumentNullException>(items != null);

            return default(Candidate<T>);
        }
        public PotentialCandidate<T> CreatePotential<T>(string contextKey, Guid reference, IEnumerable<T> items)
            where T : class
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);
            Contract.Requires<ArgumentNullException>(items != null);

            return default(PotentialCandidate<T>);
        }
    }
}
