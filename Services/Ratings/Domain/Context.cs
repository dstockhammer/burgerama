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

        public IEnumerable<Guid> Candidates
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<Guid>>() != null);
                return _candidates;
            }
        }

        public Context(string contextKey) : this(contextKey, Enumerable.Empty<Guid>()) { }

        public Context(string contextKey, IEnumerable<Guid> candidates)
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);

            ContextKey = contextKey;
            _candidates = new HashSet<Guid>(candidates);
        }

        public void AddCandidate(Guid reference)
        {
            _candidates.Add(reference);
        }
    }
}
