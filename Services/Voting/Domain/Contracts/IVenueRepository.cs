using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Voting.Domain.Contracts
{
    [ContractClass(typeof(VenueRepositoryContract))]
    public interface IVenueRepository
    {
        Venue Get(Guid venueId);

        IEnumerable<Venue> GetVotesForUser(Guid userId);

        void SaveOrUpdate(Venue venue);
    }
}
