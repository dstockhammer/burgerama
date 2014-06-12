﻿using System;
using System.Diagnostics.Contracts;
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
                TotalRating = venue.TotalRating
            };
        }

        public static Venue ToDomain(this VenueModel venue)
        {
            if (venue == null)
                return null;

            var id = Guid.Parse(venue.Id);
            return new Venue(id, venue.Name, venue.Location, venue.CreatedByUser, venue.CreatedOn)
            {
                Url = venue.Url,
                Description = venue.Description,
                Address = venue.Address,
                TotalRating = venue.TotalRating
            };
        }
    }
}
