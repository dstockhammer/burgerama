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

        void SaveOrUpdate(Outing outing);
    }
}
