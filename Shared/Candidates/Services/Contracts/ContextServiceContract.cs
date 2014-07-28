using System;
using System.Diagnostics.Contracts;

namespace Burgerama.Shared.Candidates.Services.Contracts
{
    [ContractClassFor(typeof(IContextService))]
    public abstract class ContextServiceContract : IContextService
    {
        public bool CreateContext(string contextKey, bool gracefullyHandleUnknownCandidates)
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);

            return default(bool);
        }
    }
}