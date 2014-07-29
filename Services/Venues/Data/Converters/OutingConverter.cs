using System;
using System.Diagnostics.Contracts;
using Burgerama.Services.Venues.Data.Models;
using Burgerama.Services.Venues.Domain;

namespace Burgerama.Services.Venues.Data.Converters
{
    internal static class OutingConverter
    {
        public static OutingModel ToModel(this Outing outing)
        {
            Contract.Requires<ArgumentNullException>(outing != null);

            return new OutingModel
            {
                Id = outing.Id.ToString(),
                DateTime = outing.DateTime
            };
        }

        public static Outing ToDomain(this OutingModel outing)
        {
            if (outing == null)
                return null;

            var id = Guid.Parse(outing.Id);
            return new Outing(id, outing.DateTime);

        }
    }
}
