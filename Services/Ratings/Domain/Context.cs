using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Burgerama.Services.Ratings.Domain
{
    public sealed class Context
    {
        private readonly HashSet<Guid> _candidates;

        public string ContextKey { get; private set; }

        public bool AllowToRateUnknownCandidates { get; private set; }

        public IEnumerable<Guid> Candidates
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<Guid>>() != null);
                return _candidates;
            }
        }

        public Context(string contextKey, bool allowToRateUnknownCandidates)
            : this(contextKey, allowToRateUnknownCandidates, Enumerable.Empty<Guid>())
        {
        }

        public Context(string contextKey, bool allowToRateUnknownCandidates, IEnumerable<Guid> candidates)
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);

            ContextKey = contextKey;
            AllowToRateUnknownCandidates = allowToRateUnknownCandidates;
            _candidates = new HashSet<Guid>(candidates);
        }

        public bool AddCandidate(Guid reference)
        {
            return _candidates.Add(reference);
        }
    }
}
