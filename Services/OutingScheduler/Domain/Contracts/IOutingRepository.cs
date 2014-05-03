using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.OutingScheduler.Domain.Contracts
{
    [ContractClass(typeof(OutingRepositoryContract))]
    public interface IOutingRepository
    {
        IEnumerable<Outing> GetAll();
    }
}
