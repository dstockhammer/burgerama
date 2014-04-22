﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Venues.Domain.Contracts
{
    [ContractClassFor(typeof(IVenueRepository))]
    internal abstract class VenueRepositoryContract : IVenueRepository
    {
        public Venue Get(Guid venueId)
        {
            return default(Venue);
        }

        public IEnumerable<Venue> GetAll()
        {
            Contract.Ensures(Contract.Result<IEnumerable<Venue>>() != null);

            return default(IEnumerable<Venue>);
        }

        public void SaveOrUpdate(Venue venue)
        {
            Contract.Requires<ArgumentNullException>(venue != null);
        }
    }
}