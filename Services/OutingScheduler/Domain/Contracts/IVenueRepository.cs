using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.OutingScheduler.Domain.Contracts
{
    [ContractClass(typeof(VenueRepositoryContract))]
    public interface IVenueRepository
    {
        IEnumerable<Venue> GetAll();
    }
}
