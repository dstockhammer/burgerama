using System;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Voting.Domain.Contracts
{
    [ContractClassFor(typeof(IContextRepository))]
    internal abstract class ContextRepositoryContract : IContextRepository
    {
        public Context Get(Guid contextId)
        {
            Contract.Ensures(Contract.Result<Context>() != null);

            return default(Context);
        }

        public Context GetByKey(string key)
        {
            Contract.Requires<ArgumentNullException>(key != null);
            Contract.Ensures(Contract.Result<Context>() != null);

            return default(Context);
        }

        public void SaveOrUpdate(Context context)
        {
            Contract.Requires<ArgumentNullException>(context != null);
        }
    }
}
