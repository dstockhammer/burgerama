using System;
using System.Collections.Generic;
using Burgerama.Services.Venues.Domain;

namespace Burgerama.Services.Venues.Core.Data
{
    public interface IVenueRepository
    {
        Venue Get(Guid id);

        IEnumerable<Venue> GetAll(); 

        void SaveOrUpdate(Venue venue);
    }
}
