using Burgerama.Common.MongoDb;
using Burgerama.Services.Ratings.Data.Converters;
using Burgerama.Services.Ratings.Data.Models;
using Burgerama.Services.Ratings.Domain;
using Burgerama.Services.Ratings.Domain.Contracts;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Burgerama.Services.Ratings.Data
{
    public class VenueRepository : MongoDbRepository, IVenueRepository
    {
        private MongoCollection<VenueModel> Venues
        {
            get { return GetCollection<VenueModel>("venues"); }
        }

        public Venue Get(Guid venueId)
        {
            var query = Query<VenueModel>.EQ(v => v.Id, venueId.ToString());
            var venue = Venues.FindOne(query);
            return venue != null ? venue.ToDomain() : null;
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
