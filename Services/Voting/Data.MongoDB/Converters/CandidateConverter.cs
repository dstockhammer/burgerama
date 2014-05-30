
using Burgerama.Services.Voting.Data.MongoDB.Models;
using Burgerama.Services.Voting.Domain;
using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Burgerama.Services.Voting.Data.MongoDB.Converters
{
    internal static class CandidateConverter
    {
        public static CandidateModel ToModel(this Candidate candidate, string contextKey)
        {
            Contract.Requires<ArgumentNullException>(candidate != null);
            Contract.Ensures(Contract.Result<CandidateModel>() != null);

            return new CandidateModel
            {
                Reference = candidate.Reference.ToString(),
                Votes = candidate.Votes.Select(v => v.ToModel()).ToList(),
                Expiry = candidate.Expiry,
                ContextKey = contextKey
            };
        }

        public static Candidate ToDomain(this CandidateModel candidate)
        {
            Contract.Requires<ArgumentNullException>(candidate != null);
            Contract.Ensures(Contract.Result<Candidate>() != null);

            var id = Guid.Parse(candidate.Reference);
            return new Candidate(id, candidate.Votes.Select(v => v.ToDomain()), candidate.Expiry);
        }
    }
}
