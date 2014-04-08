using System;

namespace Burgerama.Services.Venues.Core
{
    public sealed class Venue
    {
        public Guid Id { get; private set; }

        public string Title { get; private set; }

        public Venue(Guid id, string title)
        {
            Id = id;
            Title = title;
        }

        public Venue(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
        }
    }
}
