using System;
using System.Collections.Generic;
using System.Linq;
using Burgerama.Common.MongoDb;
using Burgerama.Services.Voting.Data.Converters;
using Burgerama.Services.Voting.Data.Models;
using Burgerama.Services.Voting.Domain;
using Burgerama.Services.Voting.Domain.Contracts;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace Burgerama.Services.Voting.Data
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

        public IEnumerable<Venue> GetVotesForUser(string userId)
        {
            return Venues.AsQueryable()
                .Where(v => v.Votes.Any(id => id == userId))
                .Select(m => m.ToDomain());
        }

        public void SaveOrUpdate(Venue venue)
        {
            Venues.Save(venue.ToModel());
        }
    }
}
