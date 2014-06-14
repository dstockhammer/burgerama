using Burgerama.Services.Outings.Data.Rest.Models;
using Burgerama.Services.Outings.Domain;
using System;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Outings.Data.Rest.Converters
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
                Url = venue.Url,
                Description = venue.Description,
                Address = venue.Address,
                TotalRating = venue.TotalRating
            };
        }

        public static Venue ToDomain(this VenueModel venue)
        {
            if (venue == null)
                return null;

            var id = Guid.Parse(venue.Id);
            return new Venue(id, venue.Name, venue.Location, venue.Url, venue.Description, venue.Address, venue.TotalRating);
        }
    }
}
