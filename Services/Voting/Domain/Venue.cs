using System;
using System.Collections.Generic;

namespace Burgerama.Services.Voting.Domain
{
    public class Venue
    {
        private readonly HashSet<Guid> _votes;

        public Guid Id { get; private set; }

        public Guid? Outing { get; set; }

        public IEnumerable<Guid> Votes 
        {
            get { return _votes; }
        }

        public Venue(Guid venueId)
        {
            Id = venueId;
            _votes = new HashSet<Guid>();
        }

        public Venue(Guid venueId, Guid? outingId, IEnumerable<Guid> votes)
        {
            Id = venueId;
            Outing = outingId;
            _votes = new HashSet<Guid>(votes);
        }

        /// <summary>
        /// Adds a vote to the venue.
        /// * Only one vote per user is allowed.
        /// * Voting is only allowed as long as the venue has no outing.
        /// </summary>
        /// <param name="userId">The id of the voting user.</param>
        /// <returns>Returns true if the vote was added, false otherwise.</returns>
        public bool AddVote(Guid userId)
        {
            if (Outing.HasValue)
                return false;

            return _votes.Add(userId);
        }
    }
}
