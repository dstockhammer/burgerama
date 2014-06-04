using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Burgerama.Services.Ratings.Domain
{
    public sealed class PotentialCandidate
    {
        private readonly HashSet<Rating> _ratings;

        public string ContextKey { get; private set; }

        public Guid Reference { get; private set; }
        
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

        public PotentialCandidate(string contextKey, Guid reference)
            : this(contextKey, reference, Enumerable.Empty<Rating>())
        {
        }

        public PotentialCandidate(string contextKey, Guid reference, IEnumerable<Rating> ratings)
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);
            Contract.Requires<ArgumentNullException>(ratings != null);

            var ratingsList = ratings.ToList();
            if (ratingsList.Distinct(Rating.UserComparer).Count() != ratingsList.Count())
                throw new ArgumentException();

            ContextKey = contextKey;
            Reference = reference;
            _ratings = new HashSet<Rating>(ratingsList);
        }

        /// <summary>
        /// Adds a rating to the potential candidate.
        /// </summary>
        public void AddRating(Rating rating)
        {
            Contract.Requires<ArgumentNullException>(rating != null);

            // it doesn't matter if the rating was added successfully
            _ratings.Add(rating);
        }
    }
}
