using Burgerama.Common.DataAccess.MongoDB;
using Burgerama.Services.Voting.Data.MongoDB.Converters;
using Burgerama.Services.Voting.Data.MongoDB.Models;
using Burgerama.Services.Voting.Domain;
using Burgerama.Services.Voting.Domain.Contracts;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

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
            var query = Query<ContextModel>.EQ(v => v.ContextKey, contextKey);
            var context = Contexts.FindOne(query);
            return context != null ? context.ToDomain() : null;
        }

        public void SaveOrUpdate(Context context)
        {
            Contexts.Save(context.ToModel());
        }
    }
}
