using Burgerama.Common.DataAccess;

namespace Burgerama.Services.Ratings.Tests.Data
{
    internal sealed class MongoDbTestUtils : MongoDbRepository
    {
        internal void DropVenues()
        {
            GetCollection<object>("venues").Drop();
        }
    }
}
