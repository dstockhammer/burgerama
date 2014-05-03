
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Burgerama.Services.Ratings.Core
{
    public sealed class Venue
    {
        public Guid Id { get; private set; }

        public HashSet<Rating> Ratings { get; private set; }

        public Venue(Guid id) : this(id, Enumerable.Empty<Rating>()) { }

        public Venue(Guid id, IEnumerable<Rating> ratings)
        {
            Contract.Requires<ArgumentException>(ratings != null);

            if (ratings.Distinct(Rating.UserComparer).Count() != ratings.Count()) 
                throw new ArgumentException();

            Id = id;
            Ratings = new HashSet<Rating>(ratings);
        }
    }
}
