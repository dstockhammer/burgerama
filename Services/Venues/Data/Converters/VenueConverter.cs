using System;
using Burgerama.Services.Venues.Data.Models;
using Burgerama.Services.Venues.Domain;

namespace Burgerama.Services.Venues.Data.Converters
{
    internal static class VenueConverter
    {
        public static VenueModel ToModel(this Venue venue)
        {
            return new VenueModel
            {
                Id = venue.Id.ToString(),
                Title = venue.Title,
                Location = venue.Location,
                CreatedByUser = venue.CreatedByUser,
                Url = venue.Url,
                Description = venue.Description
            };
        }

        public static Venue ToDomain(this VenueModel venue)
        {
            var id = Guid.Parse(venue.Id);

            return new Venue(id, venue.Title, venue.Location, venue.CreatedByUser)
            {
                Url = venue.Url,
                Description = venue.Description
            };
        }
    }
}
