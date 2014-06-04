using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Ratings.Domain.Contracts
{
    [ContractClassFor(typeof(ICandidateRepository))]
    internal abstract class CandidateRepositoryContract : ICandidateRepository
    {
        public Candidate Get(Guid reference, string contextKey)
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);

            return default(Candidate);
        }

        public IEnumerable<Candidate> GetAll(string contextKey)
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);
            Contract.Ensures(Contract.Result<IEnumerable<Candidate>>() != null);

            return default(IEnumerable<Candidate>);
        }

        public void SaveOrUpdate(Candidate candidate)
        {
            Contract.Requires<ArgumentNullException>(candidate != null);
        }
    }
}
