using System.Runtime.Remoting.Messaging;
using Burgerama.Common.DataAccess.MongoDB;
using Burgerama.Services.Venues.Data.Converters;
using Burgerama.Services.Venues.Data.Models;
using Burgerama.Services.Venues.Domain;
using Burgerama.Services.Venues.Domain.Contracts;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

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
            return Venues.AsQueryable()
                .SingleOrDefault(v => v.Id == venueId.ToString())
                .ToDomain();
        }

        public Venue GetByLocation(Location location)
        {
            return Venues.AsQueryable()
                .SingleOrDefault(v => v.Location.Reference == location.Reference)
                .ToDomain();
        }

        public IEnumerable<Venue> GetAll()
        {
            return Venues.FindAll()
                .Select(v => v.ToDomain());
        }

        public IEnumerable<Venue> Find(VenueQuery query)
        {
            if (query == null)
                return GetAll();

            var q = Venues.AsQueryable();
            
            if (query.HasOuting.HasValue)
                q = q.Where(v => v.Outings.Any());

            if (query.OutingId != null)
                q = q.Where(v => v.Outings.Any(o => o.Id == query.OutingId));

            if (query.CreatedByUser != null)
                q = q.Where(v => v.CreatedByUser == query.CreatedByUser);

           return q.Select(v => v.ToDomain());
        }

        public void SaveOrUpdate(Venue venue)
        {
            Venues.Save(venue.ToModel());
        }
    }
}
