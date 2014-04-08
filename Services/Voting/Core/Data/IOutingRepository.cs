using System;

namespace Burgerama.Services.Voting.Core.Data
{
    public interface IOutingRepository
    {
        bool Any(Guid venueId);

        void Add(Guid venueId);
    }
}
