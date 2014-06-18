using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Burgerama.Services.Voting.Domain
{
    public sealed class PotentialCandidate
    {
        private readonly HashSet<Vote> _votes;

        public string ContextKey { get; private set; }

        public Guid Reference { get; private set; }

        public IEnumerable<Vote> Votes
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<Vote>>() != null);
                return _votes;
            }
        }

        public PotentialCandidate(string contextKey, Guid reference)
            : this(contextKey, reference, Enumerable.Empty<Vote>())
        {
        }

        public PotentialCandidate(string contextKey, Guid reference, IEnumerable<Vote> votes)
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);
            Contract.Requires<ArgumentNullException>(votes != null);

            var votesList = votes.ToList();
            if (votesList.Distinct(Vote.UserComparer).Count() != votesList.Count())
                throw new ArgumentException();

            ContextKey = contextKey;
            Reference = reference;
            _votes = new HashSet<Vote>(votesList);
        }

        /// <summary>
        /// Adds a rating to the potential candidate.
        /// </summary>
        public void AddVote(Vote vote)
        {
            Contract.Requires<ArgumentNullException>(vote != null);

            // it doesn't matter if the rating was added successfully
            _votes.Add(vote);
        }
    }
}
