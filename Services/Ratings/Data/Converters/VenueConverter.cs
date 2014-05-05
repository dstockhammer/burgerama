using Burgerama.Services.Ratings.Data.Models;
using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Burgerama.Services.Ratings.Domain;

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
                Title = venue.Title,
                LatestOuting = venue.LatestOuting,
                Ratings = venue.Ratings.Select(r => r.ToModel())
            };
        }

        public static Venue ToDomain(this VenueModel venue)
        {
            Contract.Requires<ArgumentNullException>(venue != null);

            var venueId = Guid.Parse(venue.Id);
            var ratings = venue.Ratings.Select(r => r.ToDomain());
            return new Venue(venueId, venue.Title, ratings, venue.LatestOuting);
        }
    }
}
