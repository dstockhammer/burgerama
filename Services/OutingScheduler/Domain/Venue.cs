using System;

namespace Burgerama.Services.OutingScheduler.Domain
{
    public sealed class Venue
    {
        public Guid Id { get; private set; }

        public string Title { get; private set; }

        public int Votes { get; private set; }
        
        public Venue(Guid id, string title, int votes)
        {
            Id = id;
            Title = title;
            Votes = votes;
        }
    }
}
