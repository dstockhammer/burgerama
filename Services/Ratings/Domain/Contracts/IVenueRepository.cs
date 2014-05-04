using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Ratings.Core.Contracts
{
    [ContractClass(typeof(VenueRepositoryContract))]
    public interface IVenueRepository
    {
        Venue Get(Guid venueId);

        IEnumerable<Venue> GetAll();

        void SaveOrUpdate(Venue venue);
    }
}