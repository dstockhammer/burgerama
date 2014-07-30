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
                Name = venue.Name,
                TotalVotes = venue.TotalVotes
            };
        }

        public static Venue ToDomain(this VenueModel venue)
        {
            Contract.Requires<ArgumentNullException>(venue != null);

            var id = Guid.Parse(venue.Id);
            return new Venue(id, venue.Name, venue.TotalVotes);
        }
    }
}
