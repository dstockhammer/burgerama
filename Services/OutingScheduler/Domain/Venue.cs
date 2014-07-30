using System;

namespace Burgerama.Services.OutingScheduler.Domain
{
    public sealed class Venue
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public int TotalVotes { get; private set; }

        public Venue(Guid id, string name, int totalVotes)
        {
            Id = id;
            Name = name;
            TotalVotes = totalVotes;
        }
    }
}
