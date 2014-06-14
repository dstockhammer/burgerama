using System;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Outings.Domain
{
    public sealed class Venue
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public LocationModel Location { get; private set; }

        public string Url { get; private set; }

        public string Description { get; private set; }

        public string Address { get; private set; }

        public double? TotalRating { get; private set; }

        public Venue(Guid id, string name, LocationModel location, string url, string description, string address, double? totalRating)
        {
            Contract.Requires<ArgumentNullException>(location != null);

            Id = id;
            Name = name;
            Location = location;
            Url = url;
            Description = description;
            Address = address;
            TotalRating = totalRating;
        }
    }
}
