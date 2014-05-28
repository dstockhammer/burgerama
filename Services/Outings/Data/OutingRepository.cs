using Burgerama.Common.DataAccess.MongoDB;
using Burgerama.Services.Outings.Data.Converters;
using Burgerama.Services.Outings.Data.Models;
using Burgerama.Services.Outings.Domain;
using Burgerama.Services.Outings.Domain.Contracts;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Burgerama.Services.Outings.Data
{
    public sealed class OutingRepository : MongoDbRepository, IOutingRepository
    {
        private MongoCollection<OutingModel> Venues
        {
            get { return GetCollection<OutingModel>("outings"); }
        }

        public Outing Get(Guid outingId)
        {
            var query = Query<OutingModel>.EQ(v => v.Id, outingId.ToString());
            return Venues.FindOne(query).ToDomain();
        }

        public IEnumerable<Outing> GetAll()
        {
            return Venues.FindAll().Select(v => v.ToDomain());
        }

        public void SaveOrUpdate(Outing venue)
        {
            Venues.Save(venue.ToModel());
        }
    }
}
