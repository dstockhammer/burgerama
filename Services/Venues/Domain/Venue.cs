using System;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Venues.Domain
{
    public sealed class Venue
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public Location Location { get; private set; }

        public string CreatedByUser { get; private set; }

        public DateTime CreatedOn { get; private set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        /// <summary>
        /// todo: probably the votes shouldn't be added directly to the venue,
        /// but rather to a seperate model to make the intention clear.
        /// </summary>
        public int TotalVotes { get; set; }

        /// <summary>
        /// todo: probably the rating shouldn't be added directly to the venue,
        /// but rather to a seperate model to make the intention clear.
        /// </summary>
        public double? TotalRating { get; set; }

        public Venue(Guid id, string name, Location location, string createdByUser, DateTime createdOn)
        {
            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentNullException>(location != null);
            Contract.Requires<ArgumentNullException>(createdByUser != null);

            Id = id;
            Name = name;
            Location = location;
            CreatedByUser = createdByUser;
            CreatedOn = createdOn;
        }

        public Venue(string name, Location location, string createdByUser)
        {
            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentNullException>(location != null);
            Contract.Requires<ArgumentNullException>(createdByUser != null);

            Id = Guid.NewGuid();
            Name = name;
            Location = location;
            CreatedByUser = createdByUser;
            CreatedOn = DateTime.Now;
        }
    }
}
