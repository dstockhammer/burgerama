using System.Linq;
using Burgerama.Common.DataAccess.MongoDB;
using Burgerama.Shared.Candidates.Data.MongoDB.Converters;
using Burgerama.Shared.Candidates.Data.MongoDB.Models;
using Burgerama.Shared.Candidates.Domain;
using Burgerama.Shared.Candidates.Domain.Contracts;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Burgerama.Shared.Candidates.Data.MongoDB
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
