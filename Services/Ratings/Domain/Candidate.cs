using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Burgerama.Messaging.Events;
using Burgerama.Messaging.Events.Ratings;

namespace Burgerama.Services.Ratings.Domain
{
    public sealed class Candidate
    {
        private readonly HashSet<Rating> _ratings;

        public string ContextKey { get; private set; }

        public Guid Reference { get; private set; }

        public DateTime? OpeningDate { get; private set; }

        public DateTime? ClosingDate { get; private set; }
        
        public IEnumerable<Rating> Ratings
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<Rating>>() != null);
                return _ratings;
            }
        }

        public double? TotalRating
        {
            get
            {
                if (_ratings.Any() == false)
                    return null;

                var sum = _ratings.Aggregate(0d, (current, rating) => current + rating.Value);
                return sum / _ratings.Count();
            }
        }

        public Candidate(string contextKey, Guid reference)
            : this(contextKey, reference, Enumerable.Empty<Rating>())
        {
        }

        public Candidate(string contextKey, Guid reference, IEnumerable<Rating> ratings, DateTime? openingDate = null, DateTime? closingDate = null)
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);
            Contract.Requires<ArgumentNullException>(ratings != null);

            var ratingsList = ratings.ToList();
            if (ratingsList.Distinct(Rating.UserComparer).Count() != ratingsList.Count())
                throw new ArgumentException();

            ContextKey = contextKey;
            Reference = reference;
            OpeningDate = openingDate;
            ClosingDate = closingDate;
            _ratings = new HashSet<Rating>(ratingsList);
        }

        /// <summary>
        /// Adds a rating to the candidate.
        /// </summary>
        /// <remarks>
        /// Only one rating per user is allowed.
        /// Rating is only allowed between opening and close date.
        /// </remarks>
        /// <param name="rating">The rating.</param>
        /// <returns>Returns an <see cref="IEnumerable{T}"/> of domain events that result from this command.</returns>
        public IEnumerable<IEvent> AddRating(Rating rating)
        {
            Contract.Requires<ArgumentNullException>(rating != null);
            Contract.Ensures(Contract.Result<IEnumerable<IEvent>>() != null);

            if (AllowRatingOnDate(rating.CreatedOn) == false)
                return Enumerable.Empty<IEvent>();

            if (_ratings.Add(rating) == false)
                return Enumerable.Empty<IEvent>();

            return new IEvent[]
            {
                new RatingAdded { ContextKey = ContextKey, Reference = Reference, UserId = rating.UserId, Value = rating.Value, Text = string.Empty},
                new RatingUpdated { ContextKey = ContextKey, Reference = Reference, NewTotal = TotalRating.Value }
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
                new CandidateOpened { ContextKey = ContextKey, Reference = Reference, OpeningDate = date }
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
                new CandidateClosed { ContextKey = ContextKey, Reference = Reference, ClosingDate = date }
            };
        }

        public bool CanUserRate(string userId)
        {
            if (userId == null)
                return false;

            if (AllowRatingOnDate(DateTime.Now) == false)
                return false;

            if (Ratings.Any(r => r.UserId == userId))
                return false;

            return true;
        }

        private bool AllowRatingOnDate(DateTime date)
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
