using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Burgerama.Messaging.Events;
using Burgerama.Messaging.Events.Voting;

namespace Burgerama.Services.Voting.Domain
{
    public sealed class Venue
    {
        private readonly HashSet<string> _votes;

        public Guid Id { get; private set; }

        public DateTime? LatestOuting { get; private set; }

        public IEnumerable<string> Votes
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
                return _votes;
            }
        }

        public Venue(Guid venueId, DateTime? latestOuting = null)
        {
            Id = venueId;
            LatestOuting = latestOuting;
            _votes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        public Venue(Guid venueId, DateTime? latestOuting, IEnumerable<string> votes)
        {
            Id = venueId;
            LatestOuting = latestOuting;
            _votes = new HashSet<string>(votes, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Adds a vote to the venue.
        /// </summary>
        /// <remarks>
        /// Only one vote per user is allowed.
        /// Voting is only allowed as long as the venue has no outing.
        /// </remarks>
        /// <param name="userId">The id of the voting user.</param>
        /// <returns>Returns an <see cref="IEnumerable{T}"/> of domain events that result from this command.</returns>
        public IEnumerable<IEvent> AddVote(string userId)
        {
            Contract.Requires<ArgumentNullException>(userId != null);
            Contract.Ensures(Contract.Result<IEnumerable<IEvent>>() != null);

            if (LatestOuting.HasValue && LatestOuting.Value <= DateTime.Now)
                return Enumerable.Empty<IEvent>();

            if (_votes.Add(userId) == false)
                return Enumerable.Empty<IEvent>();

            return new IEvent[]
            {
                new VoteAdded { VenueId = Id, UserId = userId }
            };
        }

        /// <summary>
        /// Adds an outing to the venue.
        /// </summary>
        /// <remarks>
        /// Only the latest outing is saved, the history is discarded.
        /// </remarks>
        /// <param name="date">The date of the outing.</param>
        /// <returns>Returns an <see cref="IEnumerable{T}"/> of domain events that result from this command.</returns>
        public IEnumerable<IEvent> AddOuting(DateTime date)
        {
            Contract.Ensures(Contract.Result<IEnumerable<IEvent>>() != null);

            // todo: when a future outing is added to a venue that already has an outing,
            // the previous one gets overriden. this allows voting until the date of the future
            // outing. this is an issue that should be resolved in the next iteration.
            LatestOuting = date;

            return Enumerable.Empty<IEvent>();
        }
    }
}
