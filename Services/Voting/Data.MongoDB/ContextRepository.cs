using Burgerama.Common.DataAccess.MongoDB;
using Burgerama.Services.Voting.Domain.Contracts;

namespace Burgerama.Services.Voting.Data.MongoDB
{
    public sealed class ContextRepository : MongoDbRepository, IContextRepository
    {
    }
}
