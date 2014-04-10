using System;
using System.Collections.Generic;
using System.Linq;
using Burgerama.Services.Venues.Core.Data;
using Burgerama.Services.Venues.Data.Converters;
using Burgerama.Services.Venues.Data.Models;
using Burgerama.Services.Venues.Domain;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Burgerama.Services.Venues.Data.Repositories
{
    public class VenueRepository : IVenueRepository
    {
        private readonly Lazy<MongoCollection<VenueModel>> _venues;

        public VenueRepository()
        {
            // todo: move connection details into config
            const string connectionString = "localhost";
            _venues = new Lazy<MongoCollection<VenueModel>>(() => new MongoClient(connectionString).GetServer()
                .GetDatabase("burgerama_venues")
                .GetCollection<VenueModel>("venues"));
        }

        public Venue Get(Guid id)
        {
            var query = Query<VenueModel>.EQ(v => v.Id, id.ToString());
            return _venues.Value.FindOne(query).ToDomain();
        }

        public IEnumerable<Venue> GetAll()
        {
            return _venues.Value.FindAll().Select(v => v.ToDomain());
        }

        public void SaveOrUpdate(Venue venue)
        {
            _venues.Value.Save(venue.ToModel());
        }
    }
}
