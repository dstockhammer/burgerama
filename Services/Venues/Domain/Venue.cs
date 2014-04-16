using System;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Venues.Domain
{
    public sealed class Venue
    {
        public Guid Id { get; private set; }

        public string Title { get; private set; }

        public Location Location { get; private set; }

        public string CreatedByUser { get; private set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public Venue(Guid id, string title, Location location, string createdByUser)
        {
            Contract.Requires<ArgumentNullException>(location != null);

            Id = id;
            Title = title;
            Location = location;
            CreatedByUser = createdByUser;
        }

        public Venue(string title, Location location, string createdByUser)
        {
            Contract.Requires<ArgumentNullException>(location != null);

            Id = Guid.NewGuid();
            Title = title;
            Location = location;
            CreatedByUser = createdByUser;
        }
    }
}
