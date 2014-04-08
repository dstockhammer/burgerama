using System;

namespace Burgerama.Services.Voting.Core.Domain
{
    public sealed class Vote
    {
        public Guid User { get; private set; }

        public Guid Venue { get; private set; }

        public Vote(Guid user, Guid venue)
        {
            Venue = venue;
            User = user;
        }
    }
}
