using System;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Ratings.Domain.Contracts
{
    [ContractClassFor(typeof(IContextRepository))]
    internal abstract class ContextRepositoryContract : IContextRepository
    {
        public Context Get(string contextKey)
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);

            return default(Context);
        }

        public void SaveOrUpdate(Context context)
        {
            Contract.Requires<ArgumentNullException>(context != null);
        }
    }
}
