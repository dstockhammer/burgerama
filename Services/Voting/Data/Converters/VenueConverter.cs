using System;
using System.Linq;
using Burgerama.Services.Voting.Data.Models;
using Burgerama.Services.Voting.Domain;

namespace Burgerama.Services.Voting.Data.Converters
{
    internal static class VenueConverter
    {
        public static VenueModel ToModel(this Venue venue)
        {
            return new VenueModel
            {
                Id = venue.Id.ToString(),
                Outing = venue.Outing.HasValue ? venue.Outing.ToString() : null,
                Votes = venue.Votes.Select(userId => userId.ToString()).ToList()
            };
        }

        public static Venue ToDomain(this VenueModel venue)
        {
            var id = Guid.Parse(venue.Id);

            var outing = string.IsNullOrWhiteSpace(venue.Outing)
                ? (Guid?)null
                : Guid.Parse(venue.Outing);

            var votings = venue.Votes.Select(Guid.Parse);

            return new Venue(id, outing, votings);
        }
    }
}
