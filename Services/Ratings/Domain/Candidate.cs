﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Burgerama.Messaging.Events;
using Burgerama.Messaging.Events.Ratings;
using Burgerama.Shared.Candidates.Domain;

namespace Burgerama.Services.Ratings.Domain
{
    public sealed class Candidate : Candidate<Rating>
    {
        public Candidate(string contextKey, Guid reference)
            : base(contextKey, reference)
        {
        }

        public Candidate(string contextKey, Guid reference, IEnumerable<Rating> items, DateTime? openingDate = null, DateTime? closingDate = null)
            : base(contextKey, reference, items, openingDate, closingDate)
        {
        }

        public double? TotalRating
        {
            get
            {
                if (_items.Any() == false)
                    return null;

                var sum = _items.Aggregate(0d, (current, rating) => current + rating.Value);
                return sum / _items.Count();
            }
        }

        public override IEnumerable<IEvent> AddItem(Rating rating)
        {
            Contract.Requires<ArgumentNullException>(rating != null);
            Contract.Ensures(Contract.Result<IEnumerable<IEvent>>() != null);

            if (IsActiveOn(rating.CreatedOn) == false)
                return Enumerable.Empty<IEvent>();



            if (_items.Add(rating) == false)
                return Enumerable.Empty<IEvent>();

            return new IEvent[]
            {
                new RatingAdded { ContextKey = ContextKey, Reference = Reference, UserId = rating.UserId, Value = rating.Value, Text = string.Empty},
                new RatingUpdated { ContextKey = ContextKey, Reference = Reference, NewTotal = TotalRating.Value }
            };
        }

        public bool CanUserRate(string userId)
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
