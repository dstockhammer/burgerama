using Burgerama.Common.MongoDb;

namespace Burgerama.Services.Venues.Tests.Data
{
    internal sealed class MongoDbTestUtils : MongoDbRepository
    {
        internal void DropVenues()
        {
            GetCollection<object>("venues").Drop();
        }
    }
}