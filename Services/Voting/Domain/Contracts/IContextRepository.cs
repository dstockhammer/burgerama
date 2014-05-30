
using System;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Voting.Domain.Contracts
{
    [ContractClass(typeof(ContextRepositoryContract))]
    public interface IContextRepository
    {
        Context Get(Guid contextId);

        Context GetByKey(string key);

        void SaveOrUpdate(Context context);
    }
}
