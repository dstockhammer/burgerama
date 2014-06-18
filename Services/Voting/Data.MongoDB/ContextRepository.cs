using System.Linq;
using Burgerama.Common.DataAccess.MongoDB;
using Burgerama.Services.Voting.Data.MongoDB.Converters;
using Burgerama.Services.Voting.Data.MongoDB.Models;
using Burgerama.Services.Voting.Domain;
using Burgerama.Services.Voting.Domain.Contracts;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Burgerama.Services.Voting.Data.MongoDB
{
    public sealed class ContextRepository : MongoDbRepository, IContextRepository
    {
        private MongoCollection<ContextModel> Contexts
        {
            get { return GetCollection<ContextModel>("contexts"); }
        }

        public Context Get(string contextKey)
        {
            return Contexts.AsQueryable()
                .SingleOrDefault(c => c.ContextKey == contextKey)
                .ToDomain();
        }

        public void SaveOrUpdate(Context context)
        {
            Contexts.Save(context.ToModel());
        }
    }
}
