using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Voting.Domain.Contracts
{
    [ContractClassFor(typeof(IVenueRepository))]
    internal abstract class VenueRepositoryContract : IVenueRepository
    {
        public Venue Get(Guid venueId)
        {
            Contract.Ensures(Contract.Result<Venue>() != null);

            return default(Venue);
        }

        public IEnumerable<Venue> GetVotesForUser(string userId)
        {
            Contract.Requires<ArgumentNullException>(userId != null);
            Contract.Ensures(Contract.Result<IEnumerable<Venue>>() != null);

            return default(IEnumerable<Venue>);
        }

        public void SaveOrUpdate(Venue venue)
        {
            Contract.Requires<ArgumentNullException>(venue != null);
        }
    }
}
