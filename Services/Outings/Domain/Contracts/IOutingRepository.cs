using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Outings.Domain.Contracts
{
    [ContractClass(typeof(OutingRepositoryContract))]
    public interface IOutingRepository
    {
        Outing Get(Guid outingId);

        IEnumerable<Outing> GetAll();

        IEnumerable<Outing> Find(OutingQuery query);

        void SaveOrUpdate(Outing outing);
    }
}
