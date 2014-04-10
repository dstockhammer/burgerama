﻿using System;
using System.Collections.Generic;

namespace Burgerama.Services.Voting.Domain
{
    public sealed class Venue
    {
        private readonly HashSet<Guid> _votes;

        public Guid Id { get; private set; }

        public Guid? LatestOuting { get; private set; }

        public IEnumerable<Guid> Votes 
        {
            get { return _votes; }
        }

        public Venue(Guid venueId, Guid? latestOuting = null)
        {
            LatestOuting = latestOuting;
            Id = venueId;
            _votes = new HashSet<Guid>();
        }

        public Venue(Guid venueId, Guid? outingId, IEnumerable<Guid> votes, Guid? latestOuting = null)
        {
            LatestOuting = latestOuting;
            Id = venueId;
            LatestOuting = outingId;
            _votes = new HashSet<Guid>(votes);
        }

        /// <summary>
        /// Adds a vote to the venue.
        /// </summary>
        /// <remarks>
        /// Only one vote per user is allowed.
        /// Voting is only allowed as long as the venue has no outing.
        /// </remarks>
        /// <param name="userId">The id of the voting user.</param>
        /// <returns>Returns true if the vote was added, false otherwise.</returns>
        public bool AddVote(Guid userId)
        {
            if (LatestOuting.HasValue)
                return false;

            return _votes.Add(userId);
        }

        /// <summary>
        /// Adds an outing to the venue.
        /// </summary>
        /// <remarks>
        /// Only the latest outing is saved, the history is discarded.
        /// </remarks>
        /// <param name="outingId">The id of the outing.</param>
        public void AddOuting(Guid outingId)
        {
            LatestOuting = outingId;
        }
    }
}
