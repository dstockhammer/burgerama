using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Burgerama.Services.Venues.Domain
{
    public sealed class Venue
    {
        private readonly HashSet<Guid> _outings;

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public Location Location { get; private set; }

        public string CreatedByUser { get; private set; }

        public DateTime CreatedOn { get; private set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public IEnumerable<Guid> Outings
        {
            get { return _outings; }
        }

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

        public Venue(string name, Location location, string createdByUser, DateTime createdOn)
            : this(Guid.NewGuid(), name, location, createdByUser, createdOn, Enumerable.Empty<Guid>())
        {
            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentNullException>(location != null);
            Contract.Requires<ArgumentNullException>(createdByUser != null);
        }

        public Venue(Guid id, string name, Location location, string createdByUser, DateTime createdOn, IEnumerable<Guid> outings)
        {
            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentNullException>(location != null);
            Contract.Requires<ArgumentNullException>(createdByUser != null);

            Id = id;
            Name = name;
            Location = location;
            CreatedByUser = createdByUser;
            CreatedOn = createdOn;
            _outings = new HashSet<Guid>(outings);
        }

        public bool AddOuting(Guid outingId)
        {
            return _outings.Add(outingId);
        }
    }
}
