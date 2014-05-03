using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.OutingScheduler.Domain.Contracts
{
    [ContractClassFor(typeof(IVenueRepository))]
    internal abstract class VenueRepositoryContract : IVenueRepository
    {
        public IEnumerable<Venue> GetAll()
        {
            Contract.Ensures(Contract.Result<IEnumerable<Venue>>() != null);

            return default(IEnumerable<Venue>);
        }
    }
}
