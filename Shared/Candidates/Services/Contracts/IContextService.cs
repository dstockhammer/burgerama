using System.Diagnostics.Contracts;

namespace Burgerama.Shared.Candidates.Services.Contracts
{
    [ContractClass(typeof(ContextServiceContract))]
    public interface IContextService
    {
        bool CreateContext(string contextKey, bool gracefullyHandleUnknownCandidates);
    }
}