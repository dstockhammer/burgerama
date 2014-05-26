using System;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Outings.Domain
{
    public sealed class Venue
    {
        public Guid Id { get; private set; }

        public string Title { get; private set; }

        public LocationModel Location { get; private set; }

        public string Url { get; private set; }

        public string Description { get; private set; }

        public string Address { get; private set; }

        public Venue(Guid id, string title, LocationModel location, string url, string description, string address)
        {
            Contract.Requires<ArgumentNullException>(location != null);

            Id = id;
            Title = title;
            Location = location;
            Url = url;
            Description = description;
            Address = address;
        }
    }
}
