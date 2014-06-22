using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Burgerama.Services.Venues.Api.Models;
using Burgerama.Services.Venues.Domain;

namespace Burgerama.Services.Venues.Api.Converters
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
                Location = new LocationModel
                {
                    Reference = venue.Location.Reference,
                    Latitude = venue.Location.Latitude,
                    Longitude = venue.Location.Longitude
                },
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

        public static Venue ToDomain(this VenueModel venue, string userId)
        {
            Contract.Requires<ArgumentNullException>(venue != null);
            Contract.Requires<ArgumentNullException>(userId != null);

            var location = new Location(venue.Location.Reference, venue.Location.Latitude, venue.Location.Longitude);
            return new Venue(venue.Name, location, userId, venue.CreatedOn)
            {
                Description = venue.Description,
                Url = venue.Url,
                Address = venue.Address
                // do NOT set TotalVotes or TotalRating!
            };
        }
    }
}