
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Voting.Domain.Contracts
{
    [ContractClass(typeof(ContextRepositoryContract))]
    public interface IContextRepository
    {
        Context Get(string contextKey);

        void SaveOrUpdate(Context context);
    }
}
