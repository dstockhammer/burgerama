using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Burgerama.Messaging.Events;
using Burgerama.Messaging.Events.Ratings;

namespace Burgerama.Services.Ratings.Core
{
    public sealed class Venue
    {
        private readonly HashSet<Rating> _ratings;

        public Guid Id { get; private set; }
        
        public IEnumerable<Rating> Ratings
        {
            get { return _ratings; }
        }

        public double TotalRating
        {
            get
            {
                var sum = _ratings.Aggregate(0d, (current, rating) => current + rating.Value);
                return sum / _ratings.Count();
            }
        }

        public Venue(Guid id) : this(id, Enumerable.Empty<Rating>()) { }

        public Venue(Guid id, IEnumerable<Rating> ratings)
        {
            Contract.Requires<ArgumentNullException>(ratings != null);

            var ratingsList = ratings.ToList();
            if (ratingsList.Distinct(Rating.UserComparer).Count() != ratingsList.Count()) 
                throw new ArgumentException();

            Id = id;
            _ratings = new HashSet<Rating>(ratingsList);
        }

        /// <summary>
        /// Adds a rating to the venue.
        /// </summary>
        /// <remarks>
        /// Only one rating per user is allowed.
        /// todo: Rating is only allowed as for users who attended an outing with this venue.
        /// </remarks>
        /// <param name="rating">The rating.</param>
        /// <returns>Returns an <see cref="IEnumerable{T}"/> of domain events that result from this command.</returns>
        public IEnumerable<IEvent> AddRating(Rating rating)
        {
            Contract.Requires<ArgumentNullException>(rating != null);

            if (_ratings.Add(rating) == false)
                return Enumerable.Empty<IEvent>();

            return new IEvent[]
            {
                new RatingAdded { VenueId = Id, UserId = rating.User, Value = rating.Value, Text = string.Empty},
                new RatingUpdated { VenueId = Id, NewTotal = TotalRating }
            };
        }
    }
}
