using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Outings.Domain.Contracts
{
    [ContractClassFor(typeof(IOutingRepository))]
    internal abstract class OutingRepositoryContract : IOutingRepository
    {
        public Outing Get(Guid outingId)
        {
            return default(Outing);
        }

        public IEnumerable<Outing> GetAll()
        {
            Contract.Ensures(Contract.Result<IEnumerable<Outing>>() != null);

            return default(IEnumerable<Outing>);
        }

        public void SaveOrUpdate(Outing outing)
        {
            Contract.Requires<ArgumentNullException>(outing != null);
        }
    }
}