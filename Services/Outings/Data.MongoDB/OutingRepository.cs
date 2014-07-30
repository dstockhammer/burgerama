using Burgerama.Common.DataAccess.MongoDB;
using Burgerama.Services.Outings.Data.MongoDB.Converters;
using Burgerama.Services.Outings.Data.MongoDB.Models;
using Burgerama.Services.Outings.Domain;
using Burgerama.Services.Outings.Domain.Contracts;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver.Linq;

namespace Burgerama.Services.Outings.Data.MongoDB
{
    public sealed class OutingRepository : MongoDbRepository, IOutingRepository
    {
        private MongoCollection<OutingModel> Outings
        {
            get { return GetCollection<OutingModel>("outings"); }
        }

        public Outing Get(Guid outingId)
        {
            var query = Query<OutingModel>.EQ(v => v.Id, outingId.ToString());
            return Outings.FindOne(query).ToDomain();
        }

        public IEnumerable<Outing> GetAll()
        {
            return Outings.FindAll()
                .Select(v => v.ToDomain());
        }

        public IEnumerable<Outing> Find(OutingQuery query)
        {
            if (query == null)
                return GetAll();

            var q = Outings.AsQueryable();

            if (query.VenueId != null)
                q = q.Where(o => o.VenueId == query.VenueId);

            if (query.Before.HasValue)
                q = q.Where(o => o.Date < query.Before);

            if (query.After.HasValue)
                q = q.Where(o => o.Date > query.After);

            return q.Select(v => v.ToDomain());
        }

        public void SaveOrUpdate(Outing venue)
        {
            Outings.Save(venue.ToModel());
        }
    }
}
