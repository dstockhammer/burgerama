using System;
using System.Diagnostics.Contracts;

namespace Burgerama.Shared.Candidates.Services.Contracts
{
    [ContractClassFor(typeof(ICandidateService))]
    public abstract class CandidateServiceContract : ICandidateService
    {
        public void CreateCandidate(string contextKey, Guid reference, DateTime? openingDate = null, DateTime? closingDate = null)
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);
        }

        public void CloseCandidate(string contextKey, Guid reference, DateTime closingDate)
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);
        }

        public void OpenCandidate(string contextKey, Guid reference, DateTime openingDate)
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);
        }
    }
}