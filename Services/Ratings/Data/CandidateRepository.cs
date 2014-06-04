using Burgerama.Common.DataAccess.MongoDB;
using Burgerama.Services.Ratings.Data.Converters;
using Burgerama.Services.Ratings.Data.Models;
using Burgerama.Services.Ratings.Domain;
using Burgerama.Services.Ratings.Domain.Contracts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver.Linq;

namespace Burgerama.Services.Ratings.Data
{
    public class CandidateRepository : MongoDbRepository, ICandidateRepository
    {
        private MongoCollection<CandidateModel> Candidates
        {
            get { return GetCollection<CandidateModel>("candidates"); }
        }

        public Candidate Get(Guid reference, string contextKey)
        {
            return Candidates.AsQueryable()
                .SingleOrDefault(c => c.Reference == reference.ToString() && c.ContextKey == contextKey)
                .ToDomain();
        }

        public IEnumerable<Candidate> GetAll(string contextKey)
        {
            return Candidates.AsQueryable()
                .Where(c => c.ContextKey == contextKey)
                .Select(v => v.ToDomain());
        }

        public void SaveOrUpdate(Candidate candidate)
        {
            Candidates.Save(candidate.ToModel());
        }
    }
}
