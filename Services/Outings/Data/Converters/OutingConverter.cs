using System;
using System.Diagnostics.Contracts;
using Burgerama.Services.Outings.Data.Models;
using Burgerama.Services.Outings.Domain;

namespace Burgerama.Services.Outings.Data.Converters
{
    internal static class OutingConverter
    {
        public static OutingModel ToModel(this Outing outing)
        {
            Contract.Requires<ArgumentNullException>(outing != null);

            return new OutingModel
            {
                Id = outing.Id.ToString(),
                Date = outing.Date,
                Venue = outing.Venue.ToString()
            };
        }

        public static Outing ToDomain(this OutingModel outing)
        {
            Contract.Requires<ArgumentNullException>(outing != null);

            var id = Guid.Parse(outing.Id);
            var venueId = Guid.Parse(outing.Venue);

            return new Outing(id, outing.Date, venueId);
        }
    }
}
