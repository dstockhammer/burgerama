using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Burgerama.Messaging.Events;
using Burgerama.Messaging.Events.Voting;

namespace Burgerama.Services.Voting.Domain
{
    public sealed class Candidate
    {
        private readonly HashSet<Vote> _votes;

        public string ContextKey { get; private set; }

        public Guid Reference { get; private set; }

        public DateTime? OpeningDate { get; private set; }

        public DateTime? ClosingDate { get; private set; }

        public IEnumerable<Vote> Votes
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<Vote>>() != null);
                return _votes;
            }
        }

        public Candidate(string contextKey, Guid reference)
            : this(contextKey, reference, Enumerable.Empty<Vote>())
        {
        }

        public Candidate(string contextKey, Guid reference, IEnumerable<Vote> votes, DateTime? openingDate = null, DateTime? closingDate = null)
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);
            Contract.Requires<ArgumentNullException>(votes != null);

            var votesList = votes.ToList();
            if (votesList.Distinct(Vote.UserComparer).Count() != votesList.Count())
                throw new ArgumentException();

            ContextKey = contextKey;
            Reference = reference;
            OpeningDate = openingDate;
            ClosingDate = closingDate;

            _votes = new HashSet<Vote>(votesList);
        }

        /// <summary>
        /// Adds a vote to the candidate.
        /// </summary>
        /// <remarks>
        /// Only one vote per user is allowed.
        /// Voting is only allowed between opening and close date.
        /// </remarks>
        /// <param name="vote">The vote.</param>
        /// <returns>Returns an <see cref="IEnumerable{T}"/> of domain events that result from this command.</returns>
        public IEnumerable<IEvent> AddVote(Vote vote)
        {
            Contract.Requires<ArgumentNullException>(vote != null);
            Contract.Ensures(Contract.Result<IEnumerable<IEvent>>() != null);

            if (AllowVotingOnDate(vote.CreatedOn) == false)
                return Enumerable.Empty<IEvent>();

            if (_votes.Add(vote) == false)
                return Enumerable.Empty<IEvent>();

            return new IEvent[]
            {
                new VoteAdded
                {
                    // todo
                    //ContextKey = ContextKey,
                    //Reference = Reference,
                    //UserId = rating.UserId
                }
            };
        }

        /// <summary>
        /// Set the candidate to open on a specific date.
        /// </summary>
        /// <param name="date">The opening date.</param>
        /// <returns>Returns an <see cref="IEnumerable{T}"/> of domain events that result from this command.</returns>
        public IEnumerable<IEvent> OpenOn(DateTime date)
        {
            Contract.Ensures(Contract.Result<IEnumerable<IEvent>>() != null);

            // don't overwrite the date
            if (OpeningDate.HasValue)
                return Enumerable.Empty<IEvent>();

            OpeningDate = date;

            return new IEvent[]
            {
                //new CandidateOpened { ContextKey = ContextKey, Reference = Reference, OpeningDate = date }
            };
        }

        /// <summary>
        /// Set the candidate to close on a specific date.
        /// </summary>
        /// <param name="date">The close date.</param>
        /// <returns>Returns an <see cref="IEnumerable{T}"/> of domain events that result from this command.</returns>
        public IEnumerable<IEvent> CloseOn(DateTime date)
        {
            Contract.Ensures(Contract.Result<IEnumerable<IEvent>>() != null);

            // don't overwrite the date
            if (ClosingDate.HasValue)
                return Enumerable.Empty<IEvent>();

            ClosingDate = date;

            return new IEvent[]
            {
                //new CandidateClosed { ContextKey = ContextKey, Reference = Reference, ClosingDate = date }
            };
        }

        public bool CanUserVote(string userId)
        {
            if (userId == null)
                return false;

            if (AllowVotingOnDate(DateTime.Now) == false)
                return false;

            if (Votes.Any(v => v.UserId == userId))
                return false;

            return true;
        }

        private bool AllowVotingOnDate(DateTime date)
        {
            // don't allow rating if there is not opening date
            if (OpeningDate.HasValue == false)
                return false;

            // don't allow rating if candidate has not yet been opened
            if (OpeningDate.Value >= date)
                return false;

            // don't allow rating if candidate has been closed
            if (ClosingDate.HasValue && ClosingDate.Value <= date)
                return false;

            return true;
        }
    }
}
