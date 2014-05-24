using System;
using System.Diagnostics.Contracts;
using System.Security.Claims;
using Burgerama.Common.Authentication.Identity;
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
                Title = venue.Title,
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
                Rating = 0, // todo
                Votes = 0 // todo
            };
        }

        public static Venue ToDomain(this VenueModel venue, string userId)
        {
            Contract.Requires<ArgumentNullException>(venue != null);
            Contract.Requires<ArgumentNullException>(userId != null);

            var location = new Location(venue.Location.Reference, venue.Location.Latitude, venue.Location.Longitude);
            return new Venue(venue.Title, location, userId)
            {
                Description = venue.Description,
                Url = venue.Url,
                Address = venue.Address
            };
        }
    }
}