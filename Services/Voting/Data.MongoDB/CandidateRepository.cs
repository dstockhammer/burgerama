﻿using System.Collections.Generic;
using System.Linq;
using Burgerama.Common.DataAccess.MongoDB;
using Burgerama.Services.Voting.Data.MongoDB.Converters;
using Burgerama.Services.Voting.Data.MongoDB.Models;
using Burgerama.Services.Voting.Domain;
using Burgerama.Services.Voting.Domain.Contracts;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using MongoDB.Driver.Linq;

namespace Burgerama.Services.Voting.Data.MongoDB
{
    public class CandidateRepository : MongoDbRepository, ICandidateRepository
    {
        private MongoCollection<CandidateModel> Candidates
        {
            get { return GetCollection<CandidateModel>("candidates"); }
        }

        private MongoCollection<CandidateModel> PotentialCandidates
        {
            get { return GetCollection<CandidateModel>("potential_candidates"); }
        }

        public Candidate Get(string contextKey, Guid reference)
        {
            return Candidates.AsQueryable()
                .SingleOrDefault(c => c.Reference == reference.ToString() && c.ContextKey == contextKey)
                .ToDomain();
        }

        public PotentialCandidate GetPotential(string contextKey, Guid reference)
        {
            return PotentialCandidates.AsQueryable()
                .SingleOrDefault(c => c.Reference == reference.ToString() && c.ContextKey == contextKey)
                .ToPotential();
        }

        public IEnumerable<Candidate> GetAll(string contextKey)
        {
            return Candidates.AsQueryable()
                .Where(c => c.ContextKey == contextKey)
                .Select(v => v.ToDomain());
        }

        public IEnumerable<PotentialCandidate> GetAllPotential(string contextKey)
        {
            return PotentialCandidates.AsQueryable()
                .Where(c => c.ContextKey == contextKey)
                .Select(v => v.ToPotential());
        }

        public void SaveOrUpdate(Candidate candidate)
        {
            Candidates.Save(candidate.ToModel());
        }

        public void SaveOrUpdate(PotentialCandidate candidate)
        {
            PotentialCandidates.Save(candidate.ToModel());
        }

        public void Delete(PotentialCandidate candidate)
        {
            var query = Query<CandidateModel>.EQ(c => c.Reference, candidate.Reference.ToString());
            PotentialCandidates.Remove(query);
        }
    }
}
