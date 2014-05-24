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

        public DateTime CreatedOn { get; private set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public Venue(Guid id, string title, Location location, string createdByUser, DateTime createdOn)
        {
            Contract.Requires<ArgumentNullException>(location != null);
            Contract.Requires<ArgumentNullException>(createdByUser != null);

            Id = id;
            Title = title;
            Location = location;
            CreatedByUser = createdByUser;
            CreatedOn = createdOn;
        }

        public Venue(string title, Location location, string createdByUser)
        {
            Contract.Requires<ArgumentNullException>(location != null);
            Contract.Requires<ArgumentNullException>(createdByUser != null);

            Id = Guid.NewGuid();
            Title = title;
            Location = location;
            CreatedByUser = createdByUser;
            CreatedOn = DateTime.Now;
        }
    }
}
