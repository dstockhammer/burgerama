using System;

namespace Burgerama.Services.Venues.Core
{
    public sealed class Rating
    {
        public Guid User { get; private set; }

        public int Value { get; private set; }

        public Rating(Guid user, int value)
        {
            User = user;
            Value = value;
        }
    }
}