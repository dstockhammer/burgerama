using Burgerama.Common.DataAccess.MongoDB;
using Burgerama.Services.Ratings.Data.Converters;
using Burgerama.Services.Ratings.Data.Models;
using Burgerama.Services.Ratings.Domain;
using Burgerama.Services.Ratings.Domain.Contracts;
using MongoDB.Driver;
using System.Linq;
using MongoDB.Driver.Linq;

namespace Burgerama.Services.Ratings.Data
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
