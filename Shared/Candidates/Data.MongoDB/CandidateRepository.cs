using System;
using System.Collections.Generic;
using System.Linq;
using Burgerama.Common.DataAccess.MongoDB;
using Burgerama.Shared.Candidates.Data.MongoDB.Converters;
using Burgerama.Shared.Candidates.Data.MongoDB.Models;
using Burgerama.Shared.Candidates.Domain;
using Burgerama.Shared.Candidates.Domain.Contracts;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace Burgerama.Shared.Candidates.Data.MongoDB
{
    public class CandidateRepository : MongoDbRepository, ICandidateRepository
    {
        private readonly ICandidateFactory _candidateFactory;

        private MongoCollection<CandidateModel<T>> GetCandidates<T>()
            where T : class
        {
            return GetCollection<CandidateModel<T>>("candidates");
        }

        private MongoCollection<CandidateModel<T>> GetPotentialCandidates<T>()
            where T : class
        {
            return GetCollection<CandidateModel<T>>("potential_candidates");
        }

        public CandidateRepository(ICandidateFactory candidateFactory)
        {
            _candidateFactory = candidateFactory;
        }

        public Candidate<T> Get<T>(string contextKey, Guid reference)
            where T : class
        {
            return GetCandidates<T>().AsQueryable()
                .SingleOrDefault(c => c.Reference == reference.ToString() && c.ContextKey == contextKey)
                .ToDomain(_candidateFactory);
        }

        public PotentialCandidate<T> GetPotential<T>(string contextKey, Guid reference)
            where T : class
        {
            return GetPotentialCandidates<T>().AsQueryable()
                .SingleOrDefault(c => c.Reference == reference.ToString() && c.ContextKey == contextKey)
                .ToPotential(_candidateFactory);
        }

        public IEnumerable<Candidate<T>> GetAll<T>(string contextKey)
            where T : class
        {
            return GetCandidates<T>().AsQueryable()
                .Where(c => c.ContextKey == contextKey)
                .Select(v => v.ToDomain(_candidateFactory));
        }

        public IEnumerable<PotentialCandidate<T>> GetAllPotential<T>(string contextKey)
            where T : class
        {
            return GetCandidates<T>().AsQueryable()
                .Where(c => c.ContextKey == contextKey)
                .Select(v => v.ToPotential(_candidateFactory));
        }

        public void SaveOrUpdate<T>(Candidate<T> candidate)
            where T : class
        {
            GetCandidates<T>().Save(candidate.ToModel());
        }

        public void SaveOrUpdate<T>(PotentialCandidate<T> candidate)
            where T : class
        {
            GetCandidates<T>().Save(candidate.ToModel());
        }

        public void Delete<T>(PotentialCandidate<T> candidate)
            where T : class
        {
            var query = Query<CandidateModel<T>>.EQ(c => c.Reference, candidate.Reference.ToString());
            GetCandidates<T>().Remove(query);
        }
    }
}
