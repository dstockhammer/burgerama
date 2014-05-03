
using Burgerama.Services.Ratings.Core;
using Burgerama.Services.Ratings.Data.Models;
using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Burgerama.Services.Ratings.Data.Converters
{
    internal static class VenueConverter
    {
        public static VenueModel ToModel(this Venue venue)
        {
            Contract.Requires<ArgumentNullException>(venue != null);

            return new VenueModel
            {
                Id = venue.Id.ToString(),
                Ratings = venue.Ratings.Select(r => r.ToModel())
            };
        }

        public static Venue ToDomain(this VenueModel venue)
        {
            Contract.Requires<ArgumentNullException>(venue != null);

            var venueId = Guid.Parse(venue.Id);
            var ratings = venue.Ratings.Select(r => r.ToDomain());
            return new Venue(venueId, ratings);
        }
    }
}
