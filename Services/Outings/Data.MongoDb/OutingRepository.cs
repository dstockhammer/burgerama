using System;
using System.Collections.Generic;
using System.Linq;
using Burgerama.Common.MongoDb;
using Burgerama.Services.Outings.Data.MongoDb.Converters;
using Burgerama.Services.Outings.Data.MongoDb.Models;
using Burgerama.Services.Outings.Domain;
using Burgerama.Services.Outings.Domain.Contracts;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Burgerama.Services.Outings.Data.MongoDb
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
