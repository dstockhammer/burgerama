using System;

namespace Burgerama.Services.Venues.Core
{
    public sealed class Vote
    {
        public Guid User { get; private set; }

        public Vote(Guid user)
        {
            User = user;
        }
    }
}