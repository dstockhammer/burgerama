using System;
using System.Collections.Generic;

namespace Burgerama.Services.Voting.Domain
{
    public sealed class Context
    {
        public string ContextKey { get; private set; }

        public IEnumerable<Guid> Candidates { get; private set; }


        public Context(string contextKey) : this(contextKey, new List<Guid>()) {}

        public Context(string contextKey, IEnumerable<Guid> candidates)
        {
            ContextKey = contextKey;
            Candidates = candidates;
        }

        public void AddCandidate(Guid reference)
        {
            this.Candidates = new List<Guid>(this.Candidates) { reference };
        }
    }
}
