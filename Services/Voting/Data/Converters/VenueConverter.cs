using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Burgerama.Services.Voting.Domain;
using Burgerama.Services.Voting.Data.Models;

namespace Burgerama.Services.Voting.Data.Converters
{
    internal static class VenueConverter
    {
        public static VenueModel ToModel(this Venue venue)
        {
            Contract.Requires<ArgumentNullException>(venue != null);

            return new VenueModel
            {
                Id = venue.Id.ToString(),
                Outing = venue.LatestOuting.HasValue ? venue.LatestOuting.ToString() : null,
                Votes = venue.Votes.Select(userId => userId.ToString()).ToList()
            };
        }

        public static Venue ToDomain(this VenueModel venue)
        {
            Contract.Requires<ArgumentNullException>(venue != null);

            var id = Guid.Parse(venue.Id);
            var outing = string.IsNullOrWhiteSpace(venue.Outing)
                ? (Guid?)null
                : Guid.Parse(venue.Outing);

            var votings = venue.Votes.Select(Guid.Parse);

            return new Venue(id, outing, votings);
        }
    }
}
