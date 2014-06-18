using Burgerama.Common.DataAccess.MongoDB;
using Burgerama.Services.Voting.Data.MongoDB.Converters;
using Burgerama.Services.Voting.Data.MongoDB.Models;
using Burgerama.Services.Voting.Domain;
using Burgerama.Services.Voting.Domain.Contracts;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;

namespace Burgerama.Services.Voting.Data.MongoDB
{
    public sealed class CandidateRepository : MongoDbRepository, ICandidateRepository
    {
        private MongoCollection<CandidateModel> Candidates
        {
            get { return GetCollection<CandidateModel>("candidates"); }
        }

        public Candidate Get(Guid reference, string contextKey)
        {
            var query = Query.And(
                Query<CandidateModel>.EQ(c => c.Reference, reference.ToString()),
                Query<CandidateModel>.EQ(c => c.ContextKey, contextKey));

            var candidate = Candidates.FindOne(query);
            return candidate != null ? candidate.ToDomain() : null;
        }

        public void SaveOrUpdate(Candidate candidate, string contextKey)
        {
            Candidates.Save(candidate.ToModel(contextKey));
        }
    }
}
