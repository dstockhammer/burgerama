using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Venues.Domain.Contracts
{
    [ContractClass(typeof(VenueRepositoryContract))]
    public interface IVenueRepository
    {
        Venue Get(Guid venueId);

        Venue GetByLocation(Location location);

        IEnumerable<Venue> GetAll();

        IEnumerable<Venue> Find(VenueQuery query);

        void SaveOrUpdate(Venue venue);

    }
}
