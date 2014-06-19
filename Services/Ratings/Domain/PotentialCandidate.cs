using System;
using System.Collections.Generic;
using System.Linq;
using Burgerama.Shared.Candidates.Domain;

namespace Burgerama.Services.Ratings.Domain
{
    public sealed class PotentialCandidate : PotentialCandidate<Rating>
    {
        public PotentialCandidate(string contextKey, Guid reference)
            : base(contextKey, reference)
        {
        }

        public PotentialCandidate(string contextKey, Guid reference, IEnumerable<Rating> items)
            : base(contextKey, reference, items)
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
    }
}
