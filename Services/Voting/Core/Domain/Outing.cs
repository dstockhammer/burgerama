using System;

namespace Burgerama.Services.Voting.Core.Domain
{
    public class Outing
    {
        public Guid Id { get; private set; }

        public Guid Venue { get; private set; }

        public Outing(Guid venue)
        {
            Id = Guid.NewGuid();
            Venue = venue;
        }

        public Outing(Guid id, Guid venue)
        {
            Id = id;
            Venue = venue;
        }
    }
}
