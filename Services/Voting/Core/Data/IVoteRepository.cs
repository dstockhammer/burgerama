using Burgerama.Services.Voting.Core.Domain;

namespace Burgerama.Services.Voting.Core.Data
{
    public interface IVoteRepository
    {
        void Add(Vote vote);
    }
}
