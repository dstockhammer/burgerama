using Burgerama.Common.DataAccess.MongoDB;

namespace Burgerama.Services.Voting.Tests.Data
{
    internal sealed class MongoDbTestUtils : MongoDbRepository
    {
        internal void DropCandidates()
        {
            GetCollection<object>("candidates").Drop();
        }
    }
}
