﻿using System;
using System.Diagnostics.Contracts;
using Burgerama.Services.OutingScheduler.Data.Rest.Models;
using Burgerama.Services.OutingScheduler.Domain;

namespace Burgerama.Services.OutingScheduler.Data.Rest.Converters
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
                VenueId = outing.VenueId.ToString()
            };
        }

        public static Outing ToDomain(this OutingModel outing)
        {
            Contract.Requires<ArgumentNullException>(outing != null);

            var id = Guid.Parse(outing.Id);
            var venueId = Guid.Parse(outing.VenueId);

            return new Outing(id, outing.Date, venueId);
        }
    }
}
