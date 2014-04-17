using System;
using System.Collections.Generic;
using System.Linq;
using Burgerama.Common.MongoDb;
using Burgerama.Services.Venues.Data.Converters;
using Burgerama.Services.Venues.Data.Models;
using Burgerama.Services.Venues.Domain;
using Burgerama.Services.Venues.Domain.Contracts;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Burgerama.Services.Venues.Data
{
    public sealed class VenueRepository : MongoDbRepository, IVenueRepository
    {
        private MongoCollection<VenueModel> Venues
        {
            get { return GetCollection<VenueModel>("venues"); }
        }

        public Venue Get(Guid venueId)
        {
            var query = Query<VenueModel>.EQ(v => v.Id, venueId.ToString());
            return Venues.FindOne(query).ToDomain();
        }

        public IEnumerable<Venue> GetAll()
        {
            return Venues.FindAll().Select(v => v.ToDomain());
        }

        public void SaveOrUpdate(Venue venue)
        {
            Venues.Save(venue.ToModel());
        }
    }
}
