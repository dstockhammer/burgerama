using Burgerama.Services.Voting.Data.Rest.Models;
using Burgerama.Services.Voting.Domain;
using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Burgerama.Services.Voting.Data.Rest.Converters
{
    internal static class VenueConverter
    {
        public static VenueModel ToModel(this Venue venue)
        {
            Contract.Requires<ArgumentNullException>(venue != null);
            Contract.Ensures(Contract.Result<VenueModel>() != null);

            return new VenueModel
            {
                Id = venue.Id.ToString(),
                Title = venue.Title,
                LatestOuting = venue.LatestOuting,
                Votes = venue.Votes.ToList()
            };
        }

        public static Venue ToDomain(this VenueModel venue)
        {
            Contract.Requires<ArgumentNullException>(venue != null);
            Contract.Ensures(Contract.Result<Venue>() != null);

            var id = Guid.Parse(venue.Id);
            return new Venue(id, venue.Title, venue.LatestOuting, venue.Votes);
        }
    }
}
