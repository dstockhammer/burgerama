using System;
using System.Diagnostics.Contracts;
using Burgerama.Services.Outings.Api.Models;
using Burgerama.Services.Outings.Domain;

namespace Burgerama.Services.Outings.Api.Converters
{
    internal static class OutingConverter
    {
        public static OutingModel ToModel(this Outing outing, Func<Guid, Venue> venueResolver = null)
        {
            Contract.Requires<ArgumentNullException>(outing != null);

            var model = new OutingModel
            {
                Id = outing.Id.ToString(),
                Date = outing.Date,
                VenueId = outing.VenueId.ToString()
            };

            if (venueResolver != null)
                model.Venue = venueResolver(outing.VenueId).ToModel();

            return model;
        }
    }
}