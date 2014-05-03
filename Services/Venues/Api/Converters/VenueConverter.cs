using System;
using System.Diagnostics.Contracts;
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
                    Latitiude = venue.Location.Latitiude,
                    Longitude = venue.Location.Longitude
                },
                CreatedByUser = venue.CreatedByUser,
                CreatedOn = venue.CreatedOn,
                Url = venue.Url,
                Description = venue.Description,
                Rating = 0, // todo
                Votes = 0 // todo
            };
        }
    }
}