using System;
using System.Diagnostics.Contracts;
using Burgerama.Services.OutingScheduler.Data.Rest.Models;
using Burgerama.Services.OutingScheduler.Domain;

namespace Burgerama.Services.OutingScheduler.Data.Rest.Converters
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
                Votes = venue.Votes
            };
        }

        public static Venue ToDomain(this VenueModel venue)
        {
            Contract.Requires<ArgumentNullException>(venue != null);

            var id = Guid.Parse(venue.Id);
            return new Venue(id, venue.Title, venue.Votes);
        }
    }
}
