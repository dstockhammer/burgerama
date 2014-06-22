using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Burgerama.Services.Venues.Data.Models;
using Burgerama.Services.Venues.Domain;

namespace Burgerama.Services.Venues.Data.Converters
{
    internal static class VenueConverter
    {
        public static VenueModel ToModel(this Venue venue)
        {
            Contract.Requires<ArgumentNullException>(venue != null);

            return new VenueModel
            {
                Id = venue.Id.ToString(),
                Name = venue.Name,
                Location = venue.Location,
                CreatedByUser = venue.CreatedByUser,
                CreatedOn = venue.CreatedOn,
                Url = venue.Url,
                Description = venue.Description,
                Address = venue.Address,
                Outings = venue.Outings.Select(outingId => outingId.ToString()),
                TotalVotes = venue.TotalVotes,
                TotalRating = venue.TotalRating
            };
        }

        public static Venue ToDomain(this VenueModel venue)
        {
            if (venue == null)
                return null;

            var id = Guid.Parse(venue.Id);
            var outings = venue.Outings.Select(Guid.Parse);
            return new Venue(id, venue.Name, venue.Location, venue.CreatedByUser, venue.CreatedOn, outings)
            {
                Url = venue.Url,
                Description = venue.Description,
                Address = venue.Address,
                TotalVotes = venue.TotalVotes,
                TotalRating = venue.TotalRating
            };
        }
    }
}
