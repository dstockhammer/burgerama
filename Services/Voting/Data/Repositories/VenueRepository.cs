using System;
using System.Collections.Generic;
using System.Linq;
using Burgerama.Services.Voting.Core.Data;
using Burgerama.Services.Voting.Data.Converters;
using Burgerama.Services.Voting.Data.Models;
using Burgerama.Services.Voting.Domain;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace Burgerama.Services.Voting.Data.Repositories
{
    public class VenueRepository : IVenueRepository
    {
        private readonly Lazy<MongoCollection<VenueModel>> _venues;

        public VenueRepository()
        {
            // todo: move connection details into config
            const string connectionString = "localhost";
            _venues = new Lazy<MongoCollection<VenueModel>>(() => new MongoClient(connectionString).GetServer()
                .GetDatabase("burgerama_voting")
                .GetCollection<VenueModel>("venues"));
        }

        public Venue Get(Guid id)
        {
            var query = Query<VenueModel>.EQ(v => v.Id, id.ToString());
            return _venues.Value.FindOne(query).ToDomain();}

        public IEnumerable<Venue> GetVotesForUser(Guid userId)
        {
            return _venues.Value.AsQueryable()
                .Where(v => v.Votes.Any(id => id == userId.ToString()))
                .Select(m => m.ToDomain());
        }

        public void SaveOrUpdate(Venue venue)
        {
            _venues.Value.Save(venue.ToModel());
        }
    }
}
