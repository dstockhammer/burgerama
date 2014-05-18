using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Burgerama.Messaging.Events;
using Burgerama.Messaging.Events.Ratings;

namespace Burgerama.Services.Ratings.Domain
{
    public sealed class Venue
    {
        private readonly HashSet<Rating> _ratings;

        public Guid Id { get; private set; }

        public string Title { get; private set; }

        public DateTime? LatestOuting { get; private set; }
        
        public IEnumerable<Rating> Ratings
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<Rating>>() != null);
                return _ratings;
            }
        }

        public double TotalRating
        {
            get
            {
                var sum = _ratings.Aggregate(0d, (current, rating) => current + rating.Value);
                return sum / _ratings.Count();
            }
        }

        public Venue(Guid id, string title)
            : this(id, title, Enumerable.Empty<Rating>())
        {
        }

        public Venue(Guid id,  string title, IEnumerable<Rating> ratings, DateTime? latestOuting = null)
        {
            Contract.Requires<ArgumentNullException>(ratings != null);
            Contract.Requires<ArgumentNullException>(title != null);

            var ratingsList = ratings.ToList();
            if (ratingsList.Distinct(Rating.UserComparer).Count() != ratingsList.Count()) 
                throw new ArgumentException();

            Id = id;
            Title = title;
            LatestOuting = latestOuting;
            _ratings = new HashSet<Rating>(ratingsList);
        }

        /// <summary>
        /// Adds a rating to the venue.
        /// </summary>
        /// <remarks>
        /// Only one rating per user is allowed.
        /// Rating is only allowed as for users who attended an outing with this venue.
        /// </remarks>
        /// <param name="rating">The rating.</param>
        /// <returns>Returns an <see cref="IEnumerable{T}"/> of domain events that result from this command.</returns>
        public IEnumerable<IEvent> AddRating(Rating rating)
        {
            Contract.Requires<ArgumentNullException>(rating != null);
            Contract.Ensures(Contract.Result<IEnumerable<IEvent>>() != null);

            if (LatestOuting.HasValue && LatestOuting.Value <= DateTime.Now)
                return Enumerable.Empty<IEvent>();

            if (_ratings.Add(rating) == false)
                return Enumerable.Empty<IEvent>();

            return new IEvent[]
            {
                new RatingAdded { VenueId = Id, UserId = rating.User, Value = rating.Value, Text = string.Empty},
                new RatingUpdated { VenueId = Id, NewTotal = TotalRating }
            };
        }

        /// <summary>
        /// Adds an outing to the venue.
        /// </summary>
        /// <param name="date">The date of the outing.</param>
        /// <returns>Returns an <see cref="IEnumerable{T}"/> of domain events that result from this command.</returns>
        public IEnumerable<IEvent> AddOuting(DateTime date)
        {
            Contract.Ensures(Contract.Result<IEnumerable<IEvent>>() != null);
            
            // todo: when a future outing is added to a venue that already has an outing,
            // the previous one gets overriden. this prevents rating until the date of the future
            // outing. this is an issue that should be resolved in the next iteration.
            LatestOuting = date;
            
            return Enumerable.Empty<IEvent>();
        }
    }
}
