using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.OutingScheduler.Domain.Contracts
{
    [ContractClassFor(typeof(IOutingRepository))]
    internal abstract class OutingRepositoryContract : IOutingRepository
    {
        public IEnumerable<Outing> GetAll()
        {
            Contract.Ensures(Contract.Result<IEnumerable<Outing>>() != null);

            return default(IEnumerable<Outing>);
        }
    }
}
