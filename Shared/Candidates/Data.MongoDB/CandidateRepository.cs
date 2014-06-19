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

        public TCandidate Get<TCandidate, TItem>(string contextKey, Guid reference)
            where TCandidate : Candidate<TItem>
            where TItem : class
        {
            return GetCandidates<TItem>().AsQueryable()
                .SingleOrDefault(c => c.Reference == reference.ToString() && c.ContextKey == contextKey)
                .ToDomain<TCandidate, TItem>(_candidateFactory);
        }

        public TCandidate GetPotential<TCandidate, TItem>(string contextKey, Guid reference)
            where TCandidate : PotentialCandidate<TItem>
            where TItem : class
        {
            return GetPotentialCandidates<TItem>().AsQueryable()
                .SingleOrDefault(c => c.Reference == reference.ToString() && c.ContextKey == contextKey)
                .ToPotential<TCandidate, TItem>(_candidateFactory);
        }

        public IEnumerable<TCandidate> GetAll<TCandidate, TItem>(string contextKey)
            where TCandidate : Candidate<TItem>
            where TItem : class
        {
            return GetCandidates<TItem>().AsQueryable()
                .Where(c => c.ContextKey == contextKey)
                .Select(v => v.ToDomain<TCandidate, TItem>(_candidateFactory));
        }

        public IEnumerable<TCandidate> GetAllPotential<TCandidate, TItem>(string contextKey)
            where TCandidate : PotentialCandidate<TItem>
            where TItem : class
        {
            return GetCandidates<TItem>().AsQueryable()
                .Where(c => c.ContextKey == contextKey)
                .Select(v => v.ToPotential<TCandidate, TItem>(_candidateFactory));
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
