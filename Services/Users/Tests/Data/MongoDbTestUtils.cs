using Burgerama.Common.MongoDb;

namespace Burgerama.Services.Users.Tests.Data
{
    internal sealed class MongoDbTestUtils : MongoDbRepository
    {
        internal void DropUsers()
        {
            GetCollection<object>("users").Drop();
        }
    }
}
