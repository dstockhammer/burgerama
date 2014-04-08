using System;

namespace Burgerama.Services.Rating.Core
{
    public sealed class Rating
    {
        public Guid User { get; private set; }

        public Guid Venue { get; private set; }

        public decimal Value { get; private set; }

        public Rating(Guid user, Guid venue, decimal value)
        {
            User = user;
            Venue = venue;
            Value = value;
        }
    }
}
