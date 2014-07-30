using System;
using System.Diagnostics.Contracts;

namespace Burgerama.Shared.Candidates.Domain
{
    public sealed class Context
    {
        public string ContextKey { get; private set; }

        public bool GracefullyHandleUnknownCandidates { get; private set; }
        
        public Context(string contextKey, bool gracefullyHandleUnknownCandidates)
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);

            ContextKey = contextKey;
            GracefullyHandleUnknownCandidates = gracefullyHandleUnknownCandidates;
        }
    }
}
