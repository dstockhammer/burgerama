using System;
using System.Diagnostics.Contracts;
using Burgerama.Services.Outings.Api.Models;
using Burgerama.Services.Outings.Domain;

namespace Burgerama.Services.Outings.Api.Converters
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
                Location = venue.Location,
                Url = venue.Url,
                Description = venue.Description,
                Address = venue.Address
            };
        }
    }
}