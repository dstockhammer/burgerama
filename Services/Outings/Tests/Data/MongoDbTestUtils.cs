using Burgerama.Common.MongoDb;

namespace Burgerama.Services.Outings.Tests.Data
{
    internal sealed class MongoDbTestUtils : MongoDbRepository
    {
        internal void DropOutings()
        {
            GetCollection<object>("outings").Drop();
        }
    }
}
