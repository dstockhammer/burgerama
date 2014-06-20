using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Burgerama.Messaging.Events;
using Burgerama.Messaging.Events.Voting;
using Burgerama.Shared.Candidates.Domain;

namespace Burgerama.Services.Voting.Domain
{
    public sealed class Candidate : Candidate<Vote>
    {
        public Candidate(string contextKey, Guid reference)
            : base(contextKey, reference)
        {
        }

        public Candidate(string contextKey, Guid reference, IEnumerable<Vote> items, DateTime? openingDate = null, DateTime? closingDate = null)
            : base(contextKey, reference, items, openingDate, closingDate)
        {
        }

        public override IEnumerable<IEvent> OnCreateSuccess()
        {
            return new IEvent[]
            {
                new CandidateCreated { ContextKey = ContextKey, Reference = Reference }
            };
        }

        public override IEnumerable<IEvent> AddItem(Vote vote)
        {
            Contract.Requires<ArgumentNullException>(vote != null);
            Contract.Ensures(Contract.Result<IEnumerable<IEvent>>() != null);

            if (IsActiveOn(vote.CreatedOn) == false)
                return Enumerable.Empty<IEvent>();

            if (CanUserVote(vote.UserId) == false)
                return Enumerable.Empty<IEvent>();

            if (_items.Add(vote) == false)
                return Enumerable.Empty<IEvent>();

            return new IEvent[]
            {
                new VoteAdded { ContextKey = ContextKey, Reference = Reference, UserId = vote.UserId }
            };
        }

        protected override IEnumerable<IEvent> OnOpeningSuccess(DateTime date)
        {
            return new IEvent[]
            {
                new CandidateOpened { ContextKey = ContextKey, Reference = Reference, OpeningDate = date }
            };
        }

        protected override IEnumerable<IEvent> OnClosingSuccess(DateTime date)
        {
            return new IEvent[]
            {
                new CandidateClosed { ContextKey = ContextKey, Reference = Reference, ClosingDate = date }
            };
        }

        public bool CanUserVote(string userId)
        {
            if (userId == null)
                return false;

            if (IsActiveOn(DateTime.Now) == false)
                return false;

            if (Items.Any(r => r.UserId == userId))
                return false;

            return true;
        }
    }
}
