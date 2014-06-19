using System;
using System.Diagnostics.Contracts;

namespace Burgerama.Shared.Candidates.Services.Contracts
{
    [ContractClass(typeof(CandidateServiceContract))]
    public interface ICandidateService
    {
        void CreateCandidate(string contextKey, Guid reference, DateTime? openingDate = null, DateTime? closingDate = null);

        void CloseCandidate(string contextKey, Guid reference, DateTime closingDate);

        void OpenCandidate(string contextKey, Guid reference, DateTime openingDate);
    }
}
