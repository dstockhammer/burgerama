using Burgerama.Services.Voting.Api.Models;
using Burgerama.Services.Voting.Domain;
using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Burgerama.Services.Voting.Api.Converters
{
    internal static class CandidateConverter
    {
        public static CandidateModel ToModel(this Candidate candidate)
        {
            Contract.Requires<ArgumentNullException>(candidate != null);

            return new CandidateModel
            {
                Reference = candidate.Reference,
                Expiry = candidate.Expiry,
                Votes = candidate.Votes.Select(v => v.ToModel())
            };
        }

        public static Candidate ToDomain(this CandidateModel candidate)
        {
            Contract.Requires<ArgumentNullException>(candidate != null);

            return new Candidate(candidate.Reference, candidate.Votes.Select(v => v.ToDomain()), candidate.Expiry);
        }
    }
}