using System;
using System.Collections.Generic;
using Burgerama.Shared.Candidates.Domain;

namespace Burgerama.Services.Voting.Domain
{
    public sealed class PotentialCandidate : PotentialCandidate<Vote>
    {
        public PotentialCandidate(string contextKey, Guid reference)
            : base(contextKey, reference)
        {
        }

        public PotentialCandidate(string contextKey, Guid reference, IEnumerable<Vote> items)
            : base(contextKey, reference, items)
        {
        }
    }
}
